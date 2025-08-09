(function () {
  if (!window.tinymce) return;

  // Register a custom button that opens a Bootstrap modal for print options
  function registerCustomPrint(editor) {
    editor.ui.registry.addButton('customprint', {
      text: 'Print…',
      tooltip: 'Custom Print Options',
      onAction: function () {
        const modal = document.getElementById('customPrintModal');
        if (!modal) {
          alert('Custom Print modal not found. Ensure the modal HTML exists in the page.');
          return;
        }
        // Reset form defaults if needed
        document.getElementById('printPageSize').value = 'A4';
        document.getElementById('printMargin').value = '20';
        document.getElementById('printHeader').value = '';
        document.getElementById('printWatermark').value = '';

        // Show modal (Bootstrap 5)
        if (window.bootstrap && bootstrap.Modal) {
          const bsModal = new bootstrap.Modal(modal);
          bsModal.show();
        } else {
          modal.style.display = 'block';
        }
      }
    });
  }

  tinymce.init({
    selector: '.editor',
    base_url: '/lib/tinymce',
    suffix: '.min',
    height: 520,
    menubar: 'file edit view insert format tools table help',
    plugins: 'lists advlist table link image charmap searchreplace visualblocks visualchars wordcount autolink anchor pagebreak print',
    toolbar: [
      'undo redo | blocks fontselect fontsizeselect | bold italic underline strikethrough subscript superscript | forecolor backcolor',
      'alignleft aligncenter alignright alignjustify | outdent indent | bullist numlist | table link image charmap | pagebreak',
      'searchreplace | removeformat | visualblocks visualchars | wordcount | print customprint'
    ].join(' '),
    branding: false,
    browser_spellcheck: true,
    contextmenu: false,
    convert_urls: false,
    paste_data_images: false,
    image_caption: true,
    table_toolbar: 'tableprops tabledelete | tableinsertrowbefore tableinsertrowafter tabledeleterow | tableinsertcolbefore tableinsertcolafter tabledeletecol',
    block_formats: 'Paragraph=p; Heading 1=h1; Heading 2=h2; Heading 3=h3',
    content_css: ['/css/print.css'],
    setup: function (editor) {
      // Custom button
      registerCustomPrint(editor);

      // Basic MS Word paste cleanup (without PowerPaste)
      editor.on('PastePostProcess', function (e) {
        // Strip MSO classes
        e.node.innerHTML = e.node.innerHTML.replace(/class="Mso[a-zA-Z0-9\s]*"/g, '');
        // Remove comments
        e.node.innerHTML = e.node.innerHTML.replace(/<!--(.*?)-->/g, '');
        // Remove Office-specific tags
        e.node.innerHTML = e.node.innerHTML.replace(/<(o|v|w):[^>]+>/g, '');
      });
    }
  });

  // Handle Custom Print form submit
  document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('customPrintForm');
    if (!form) return;
    form.addEventListener('submit', function (ev) {
      ev.preventDefault();
      const editor = tinymce.activeEditor;
      if (!editor) return;

      const size = document.getElementById('printPageSize').value || 'A4';
      const margin = document.getElementById('printMargin').value || '20';
      const header = document.getElementById('printHeader').value || '';
      const watermark = document.getElementById('printWatermark').value || '';

      const html = editor.getContent({ format: 'html' });
      const win = window.open('', '_blank');
      win.document.write(`<!doctype html>
<html>
<head>
<meta charset="utf-8">
<title>Print</title>
<style>
  @page { size: ${size}; margin: ${margin}mm; }
  @media print { .no-print { display:none } }
  body { font-family: "Times New Roman", serif; font-size: 12pt; line-height: 1.4; }
  header.print-header { position: fixed; top: 0; left:0; right:0; text-align:center; padding:6mm 0; }
  footer.print-footer { position: fixed; bottom: 0; left:0; right:0; text-align:center; padding:6mm 0; }
  main { margin: 20mm 0; } /* leave room for header/footer */
  ${watermark ? `body::before {
    content: '${watermark.replace(/'/g,"\\'")}';
    position: fixed; top: 40%; left: 50%;
    transform: translate(-50%, -50%) rotate(-30deg);
    opacity: 0.08; font-size: 120pt; pointer-events: none;
    white-space: nowrap;
  }` : ''}
</style>
<link rel="stylesheet" href="/css/print.css">
</head>
<body>
  ${header ? `<header class="print-header">${header}</header>` : ''}
  <main>${html}</main>
  <script>window.onload = () => window.print();<\/script>
</body>
</html>`);
      win.document.close();

      // Hide modal
      const modalEl = document.getElementById('customPrintModal');
      if (window.bootstrap && bootstrap.Modal && modalEl) {
        bootstrap.Modal.getOrCreateInstance(modalEl).hide();
      } else if (modalEl) {
        modalEl.style.display = 'none';
      }
    });
  });
})();