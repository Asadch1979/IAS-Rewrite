import re
import csv
from pathlib import Path

BASE_DIR = Path(__file__).resolve().parent
VIEWS_DIR = BASE_DIR / 'AIS' / 'AIS' / 'Views'
CONTROLLERS_DIR = BASE_DIR / 'AIS' / 'AIS' / 'Controllers'
OUTPUT_FILE = BASE_DIR / 'AIS' / 'AIS' / 'wwwroot' / 'All Appicalls.csv'

DB_CALL_RE = re.compile(r'\b[dD]BConnection\.([A-Za-z_][A-Za-z0-9_]*)\s*\(')
APICALL_RE = re.compile(r'\b(?:Appicalls|ApiCalls)\.([A-Za-z_][A-Za-z0-9_]*)\s*\(')
AJAX_URL_RE = re.compile(r'\$\.ajax\(\s*{[^}]*?url\s*:\s*[\'\"]([^\'\"]+)[\'\"]', re.IGNORECASE)
JQ_GET_RE = re.compile(r'\$\.get\(\s*[\'\"]([^\'\"]+)[\'\"]', re.IGNORECASE)
JQ_POST_RE = re.compile(r'\$\.post\(\s*[\'\"]([^\'\"]+)[\'\"]', re.IGNORECASE)
FETCH_RE = re.compile(r'fetch\(\s*[\'\"]([^\'\"]+)[\'\"]', re.IGNORECASE)
AXIOS_RE = re.compile(r'axios\.(get|post|put|delete)\(\s*[\'\"]([^\'\"]+)[\'\"]', re.IGNORECASE)

def find_db_calls(controller_path: Path, action_name: str):
    if not controller_path.exists():
        return []
    lines = controller_path.read_text(encoding='utf-8', errors='ignore').splitlines()
    results = []
    inside = False
    brace_level = 0
    for line in lines:
        if not inside:
            if re.search(rf'\b{re.escape(action_name)}\s*\(', line):
                inside = True
                brace_level = line.count('{') - line.count('}')
                results.extend(DB_CALL_RE.findall(line))
        else:
            brace_level += line.count('{') - line.count('}')
            results.extend(DB_CALL_RE.findall(line))
            if brace_level <= 0:
                break
    if results:
        return results
    return DB_CALL_RE.findall('\n'.join(lines))

def find_app_calls(view_path: Path):
    text = view_path.read_text(encoding='utf-8', errors='ignore')
    results = []
    results.extend(APICALL_RE.findall(text))
    results.extend(AJAX_URL_RE.findall(text))
    results.extend(JQ_GET_RE.findall(text))
    results.extend(JQ_POST_RE.findall(text))
    results.extend(FETCH_RE.findall(text))
    results.extend([url for _, url in AXIOS_RE.findall(text)])
    return results

def main():
    rows = []
    for view in VIEWS_DIR.rglob('*.cshtml'):
        view_name = view.stem
        controller_folder = view.parent.name
        controller_name = controller_folder + 'Controller'
        controller_path = CONTROLLERS_DIR / f'{controller_folder}Controller.cs'

        for call in find_db_calls(controller_path, view_name):
            rows.append({'Controller': controller_name, 'View': view_name, 'Dbconnection': call, 'Appicalls': ''})

        for call in find_app_calls(view):
            rows.append({'Controller': controller_name, 'View': view_name, 'Dbconnection': '', 'Appicalls': call})

    if rows:
        OUTPUT_FILE.parent.mkdir(parents=True, exist_ok=True)
        write_header = not OUTPUT_FILE.exists()
        with OUTPUT_FILE.open('a', newline='', encoding='utf-8') as fh:
            writer = csv.DictWriter(fh, fieldnames=['Controller', 'View', 'Dbconnection', 'Appicalls'])
            if write_header:
                writer.writeheader()
            writer.writerows(rows)

if __name__ == '__main__':
    main()
