function initFieldAuditParaSection(config) {
    var opts = $.extend({
        containerSelector: '#fieldAuditParaSection',
        readOnly: false,
        annexList: []
    }, config || {});

    var container = $(opts.containerSelector);

    var annexureSel = container.find('#auditPara_Annex');
    var riskDisplay = container.find('#viewMemo_risk_display');
    var processSel = container.find('#auditPara_Process');
    var subProcessSel = container.find('#auditPara_SubProcess');
    var checklistSel = container.find('#auditPara_Checklist');
    var gistField = container.find('#auditPara_Gist');
    var paraTextField = container.find('#paraTextViewer');
    var paraTextEditor = null;

    var selectedRiskId = 0;

    function updateRiskDisplay() {
        var annexId = annexureSel.val();
        var riskName = '';
        selectedRiskId = 0;
        $.each(opts.annexList, function (i, v) {
            var id = v.ID || v.id;
            if (id == annexId) {
                riskName = v.RISK || v.risk;
                selectedRiskId = v.RISK_ID || v.risK_ID;
            }
        });
        riskDisplay.val(riskName);
        var color = '';
        if (riskName && riskName.toLowerCase() === 'high') {
            color = 'red';
        } else if (riskName && riskName.toLowerCase() === 'medium') {
            color = 'gold';
        } else if (riskName && riskName.toLowerCase() === 'low') {
            color = 'green';
        }
        riskDisplay.css('color', color);
    }

    function loadSubProcess() {
        if (!subProcessSel.length) return;
        subProcessSel.empty().append('<option value="0" id="0">--Select Sub-Process--</option>');
        if (processSel.length && processSel.val() != 0) {
            $.ajax({
                url: g_asiBaseURL + '/Setup/process_details',
                type: 'POST',
                data: { 'ProcessId': processSel.val() },
                dataType: 'json',
                success: function (data) {
                    $.each(data, function (index, pid) {
                        subProcessSel.append('<option value="' + pid.id + '" id="' + pid.id + '">' + pid.title + '</option>');
                    });
                }
            });
        }
    }

    function loadChecklist() {
        if (!checklistSel.length) return;
        checklistSel.empty().append('<option value="0" id="0">--Select Checklist Detail--</option>');
        if (subProcessSel.length && subProcessSel.val() != 0) {
            $.ajax({
                url: g_asiBaseURL + '/Setup/process_transactions',
                type: 'POST',
                data: { 'ProcessDetailId': subProcessSel.val() },
                dataType: 'json',
                success: function (data) {
                    $.each(data, function (index, clid) {
                        checklistSel.append('<option value="' + clid.id + '" id="' + clid.id + '">' + clid.description + '</option>');
                    });
                }
            });
        }
    }

    function initEditor() {
        if (paraTextField.length && typeof CKEDITOR !== 'undefined') {
            paraTextEditor = CKEDITOR.replace(paraTextField.attr('id'), {
                extraPlugins: 'print',
                height: 500,
                toolbar: [
                    { name: 'document', items: ['Source', 'Preview', 'Print', 'PageBreak'] },
                    { name: 'clipboard', items: ['Undo', 'Redo', 'Find', 'Replace', 'SelectAll', 'RemoveFormat'] },
                    { name: 'styles', items: ['Format', 'Font', 'FontSize'] },
                    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'CopyFormatting'] },
                    { name: 'colors', items: ['TextColor', 'BGColor'] },
                    { name: 'paragraph', items: ['NumberedList', 'BulletedList', 'Outdent', 'Indent', 'Blockquote', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
                    { name: 'insert', items: ['Table', 'SpecialChar', 'HorizontalRule', 'Link', 'Unlink'] },
                    { name: 'tools', items: ['Maximize'] }
                ],
                removePlugins: 'cloudservices,easyimage',
                extraAllowedContent: true
            });
        }
    }

    function setReadOnly(val) {
        opts.readOnly = val;
        var fields = [annexureSel, processSel, subProcessSel, checklistSel, gistField];
        $.each(fields, function (i, f) {
            if (f.length) f.prop('disabled', val);
        });
        if (paraTextEditor) {
            paraTextEditor.setReadOnly(val);
        } else if (paraTextField.length) {
            paraTextField.prop('disabled', val);
        }
    }

    function getData() {
        return {
            annexureId: annexureSel.val(),
            riskId: selectedRiskId,
            processId: processSel.val(),
            subProcessId: subProcessSel.val(),
            checklistId: checklistSel.val(),
            gist: gistField.val(),
            paraText: paraTextEditor ? paraTextEditor.getData() : paraTextField.val()
        };
    }

    function loadData(d) {
        if (!d) return;
        annexureSel.val(d.annexureId);
        updateRiskDisplay();
        if (processSel.length) processSel.val(d.processId);
        loadSubProcess();
        if (subProcessSel.length) subProcessSel.val(d.subProcessId);
        loadChecklist();
        if (checklistSel.length) checklistSel.val(d.checklistId);
        gistField.val(d.gist);
        if (paraTextEditor) {
            paraTextEditor.setData(d.paraText || '');
        } else {
            paraTextField.val(d.paraText).trigger('change');
        }
    }

    annexureSel.off('change.fap').on('change.fap', updateRiskDisplay);
    processSel.off('change.fap').on('change.fap', loadSubProcess);
    subProcessSel.off('change.fap').on('change.fap', loadChecklist);

    updateRiskDisplay();
    initEditor();
    setReadOnly(opts.readOnly);

    return {
        updateRiskDisplay: updateRiskDisplay,
        reloadSubProcess: loadSubProcess,
        reloadChecklist: loadChecklist,
        setReadOnly: setReadOnly,
        getData: getData,
        loadData: loadData
    };
}

// ===== CKEditor central initializer (auto-generated) =====
(function(){
  function log(msg){ if (window.console) console.log('[CKInit]', msg); }
  function warn(msg){ if (window.console) console.warn('[CKInit]', msg); }
  function err(msg){ if (window.console) console.error('[CKInit]', msg); }

  function ensureCkAvailable(){
    if (typeof CKEDITOR === 'undefined') {
      err('CKEditor not found. Include ~/lib/ckeditor/ckeditor.js in the page BEFORE this script.');
      return false;
    }
    return true;
  }

  function initOne(id, config){
    var el = document.getElementById(id);
    if (!el) { warn('Textarea not found: #' + id); return; }
    var inst = CKEDITOR.instances[id];
    if (inst) {
      var instEl = inst.element && inst.element.$;
      var active = instEl === el && inst.status !== 'destroyed' && inst.status !== 'destroying';
      if (active) {
        log('Already initialized #' + id);
        return;
      }
      try { inst.destroy(true); } catch(e){ warn('Destroy failed for #' + id + ': ' + e); }
    }
    CKEDITOR.replace(id, config);
    log('Initialized #' + id);
  }

  function buildConfig(){
    return {
      height: 500,
      toolbar: [
        { name: 'document',   items: ['Source','Preview','Print','PageBreak'] },
        { name: 'clipboard',  items: ['Undo','Redo','Find','Replace','SelectAll','RemoveFormat'] },
        { name: 'styles',     items: ['Format','Font','FontSize'] },
        { name: 'basicstyles',items: ['Bold','Italic','Underline','Strike','Subscript','Superscript','CopyFormatting'] },
        { name: 'colors',     items: ['TextColor','BGColor'] },
        { name: 'paragraph',  items: ['NumberedList','BulletedList','Outdent','Indent','Blockquote','JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'] },
        { name: 'insert',     items: ['Table','SpecialChar','HorizontalRule','Link','Unlink'] },
        { name: 'tools',      items: ['Maximize'] }
      ],
      pasteFromWordPromptCleanup: true,
      pasteFromWordRemoveFontStyles: false,
      pasteFromWordRemoveStyles: false,
      contentsCss: ['/css/print.css'],
      removePlugins: 'cloudservices,easyimage',
      extraAllowedContent: true
    };
  }

  function initAll(){
    if (!ensureCkAvailable()) return;
    var cfg = buildConfig();

    ['AuditParaHtml','auditPara_Gist','template_box','viewMemo_memo'].forEach(function(id){
      initOne(id, cfg);
    });

    var custom = document.querySelectorAll('textarea[data-editor="ck"]');
    custom.forEach(function(t){
      var id = t.id || '';
      if (!id) { warn('textarea[data-editor="ck"] has no id'); return; }
      initOne(id, cfg);
    });
  }

  window.FieldAuditEditors = window.FieldAuditEditors || {};
  window.FieldAuditEditors.init = initAll;

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initAll);
  } else {
    initAll();
  }
  if (window.jQuery) {
    jQuery(function(){ initAll(); });
    jQuery(document).on('shown.bs.modal', function(){ initAll(); });
  }

  try{
    var mo = new MutationObserver(function(muts){
      for (var i=0;i<muts.length;i++){
        if (muts[i].addedNodes && muts[i].addedNodes.length){
          initAll();
          break;
        }
      }
    });
    mo.observe(document.documentElement, {childList:true, subtree:true});
  }catch(_){/* ignore */}
})();
