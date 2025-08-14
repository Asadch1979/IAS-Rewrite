import re
import csv
from itertools import product
from pathlib import Path

BASE_DIR = Path(__file__).resolve().parent
VIEWS_DIR = BASE_DIR / 'AIS' / 'AIS' / 'Views'
CONTROLLERS_DIR = BASE_DIR / 'AIS' / 'AIS' / 'Controllers'
OUTPUT_FILE = BASE_DIR / 'AIS' / 'AIS' / 'wwwroot' / 'All Appicalls.csv'

DB_CALL_RE = re.compile(r'\b[dD]BConnection\.([A-Za-z_][A-Za-z0-9_]*)\s*\(')
STORED_PROC_RE = re.compile(r'cmd\.CommandText\s*=\s*["\']([^"\']+)["\']')
PAGE_ID_RE = re.compile(r'var\s+page_id\s*=\s*["\']?(\d+)["\']?')

APICALL_RE = re.compile(r'\b(?:Appicalls|ApiCalls)\.([A-Za-z_][A-Za-z0-9_]*)\s*\(')
AJAX_URL_RE = re.compile(r'\$\.ajax\(\s*{[^}]*?url\s*:\s*[\'\"]([^\'\"]+)[\'\"]', re.IGNORECASE)
AJAX_CONCAT_RE = re.compile(r'url\s*:\s*g_asiBaseURL\s*\+\s*["\']([^"\']+)["\']', re.IGNORECASE)
JQ_GET_RE = re.compile(r'\$\.get\(\s*[\'\"]([^\'\"]+)[\'\"]', re.IGNORECASE)
JQ_POST_RE = re.compile(r'\$\.post\(\s*[\'\"]([^\'\"]+)[\'\"]', re.IGNORECASE)
FETCH_RE = re.compile(r'fetch\(\s*[\'\"]([^\'\"]+)[\'\"]', re.IGNORECASE)
AXIOS_RE = re.compile(r'axios\.(get|post|put|delete)\(\s*[\'\"]([^\'\"]+)[\'\"]', re.IGNORECASE)
CONCAT_URL_RE = re.compile(r'g_asiBaseURL\s*\+\s*["\']([^"\']+)["\']', re.IGNORECASE)

def _parse_db_section(lines):
    """Return list of (db_call, stored_proc) from a list of lines."""
    found = []
    for line in lines:
        for call in DB_CALL_RE.findall(line):
            found.append([call, ""])
        sp_match = STORED_PROC_RE.search(line)
        if sp_match:
            if found and not found[-1][1]:
                found[-1][1] = sp_match.group(1)
            else:
                found.append(["", sp_match.group(1)])
    return found


def find_db_calls(controller_path: Path, action_name: str):
    if not controller_path.exists():
        return []
    lines = controller_path.read_text(encoding="utf-8", errors="ignore").splitlines()
    inside = False
    brace_level = 0
    segment = []
    for line in lines:
        if not inside:
            if re.search(rf"\b{re.escape(action_name)}\s*\(", line):
                inside = True
                brace_level = line.count("{") - line.count("}")
                segment = [line]
        else:
            brace_level += line.count("{") - line.count("}")
            segment.append(line)
            if brace_level <= 0:
                return _parse_db_section(segment)
    # fallback to whole file
    return _parse_db_section(lines)

def find_app_calls(view_path: Path):
    text = view_path.read_text(encoding="utf-8", errors="ignore")
    results = []
    results.extend(APICALL_RE.findall(text))
    results.extend(AJAX_URL_RE.findall(text))
    results.extend(AJAX_CONCAT_RE.findall(text))
    results.extend(JQ_GET_RE.findall(text))
    results.extend(JQ_POST_RE.findall(text))
    results.extend(FETCH_RE.findall(text))
    results.extend([url for _, url in AXIOS_RE.findall(text)])
    results.extend(CONCAT_URL_RE.findall(text))
    return results


def find_page_id(view_path: Path):
    text = view_path.read_text(encoding="utf-8", errors="ignore")
    m = PAGE_ID_RE.search(text)
    return m.group(1) if m else ""

def main():
    rows = []
    for view in VIEWS_DIR.rglob("*.cshtml"):
        view_name = view.stem
        controller_folder = view.parent.name
        controller_path = CONTROLLERS_DIR / f"{controller_folder}Controller.cs"
        page_id = find_page_id(view)

        db_calls = find_db_calls(controller_path, view_name)
        app_calls = find_app_calls(view)

        if db_calls and app_calls:
            for (db_call, sp), app in product(db_calls, app_calls):
                rows.append({
                    "Controller": controller_folder,
                    "View": view_name,
                    "page_id": page_id,
                    "Dbconnection": db_call,
                    "Stored Procedure for controller": sp,
                    "Appicalls": app,
                    "Stored Procedure for Appicalls": "",
                })
        elif db_calls:
            for db_call, sp in db_calls:
                rows.append({
                    "Controller": controller_folder,
                    "View": view_name,
                    "page_id": page_id,
                    "Dbconnection": db_call,
                    "Stored Procedure for controller": sp,
                    "Appicalls": "",
                    "Stored Procedure for Appicalls": "",
                })
        elif app_calls:
            for app in app_calls:
                rows.append({
                    "Controller": controller_folder,
                    "View": view_name,
                    "page_id": page_id,
                    "Dbconnection": "",
                    "Stored Procedure for controller": "",
                    "Appicalls": app,
                    "Stored Procedure for Appicalls": "",
                })

    if rows:
        OUTPUT_FILE.parent.mkdir(parents=True, exist_ok=True)
        with OUTPUT_FILE.open("w", newline="", encoding="utf-8") as fh:
            writer = csv.DictWriter(
                fh,
                fieldnames=[
                    "Controller",
                    "View",
                    "page_id",
                    "Dbconnection",
                    "Stored Procedure for controller",
                    "Appicalls",
                    "Stored Procedure for Appicalls",
                ],
            )
            writer.writeheader()
            writer.writerows(rows)

if __name__ == '__main__':
    main()
