function initResponsibilitySection(config) {
    var opts = $.extend({
        tableSelector: '#listofRespPersons',
        changesTableSelector: '#c_listofRespPersons',
        modalSelector: '#ResponsiblePPModel',
        comId: 0,
        newParaId: 0,
        oldParaId: 0,
        indicator: '',
        readOnly: false
    }, config || {});

    var table = $(opts.tableSelector);
    var changesTable = $(opts.changesTableSelector);
    var modal = $(opts.modalSelector);
    var respUser = [];
    var selectedRow = null;

    function load() {
        if (!table.length) return;
        table.find('tbody').empty();
        if (changesTable.length) changesTable.find('tbody').empty();
        $.ajax({
            url: g_asiBaseURL + '/ApiCalls/get_responsible_person_list',
            type: 'POST',
            data: {
                'PARA_ID': opts.newParaId ? opts.newParaId : opts.oldParaId,
                'INDICATOR': opts.indicator,
                'COM_ID': opts.comId
            },
            dataType: 'json',
            success: function (data) {
                var sr = 1; var sr_c = 1;
                $.each(data, function (i, v) {
                    var row = '<tr id="tr_' + v.pP_NO + '"><td>' + sr + '</td><td>' + v.pP_NO + '</td><td>' + v.emP_NAME + '</td><td>' + v.loaN_CASE + '</td><td>' + v.lC_AMOUNT + '</td><td>' + v.accounT_NUMBER + '</td><td>' + v.acC_AMOUNT + '</td><td>' + v.remarks + '</td>';
                    if (!opts.readOnly && v.indicator === 'O')
                        row += '<td class="text-center"><a href="#" class="updateResp">Update / delete</a></td>';
                    row += '</tr>';
                    if (v.indicator === 'O') {
                        table.find('tbody').append(row); sr++; }
                    else if (changesTable.length) {
                        changesTable.find('tbody').append('<tr id="tr_' + v.pP_NO + '"><td>' + sr_c + '</td><td>' + v.pP_NO + '</td><td>' + v.emP_NAME + '</td><td>' + v.loaN_CASE + '</td><td>' + v.lC_AMOUNT + '</td><td>' + v.accounT_NUMBER + '</td><td>' + v.acC_AMOUNT + '</td><td>' + v.remarks + '</td></tr>'); sr_c++; }
                });
            }
        });
    }

    function getMatchedPP() {
        $('#matchedPPNoPanels').empty();
        respUser = [];
        $.ajax({
            url: g_asiBaseURL + '/ApiCalls/get_employee_name_from_pp',
            type: 'POST',
            data: { 'PP_NO': $('#responsiblePPNoEntryField').val() },
            dataType: 'json',
            success: function (data) {
                respUser.push(data);
                if (data.ppNumber > 0) {
                    $('#matchedPPNoPanels').append('<div class="row"><div class="row col-md-12 mt-2"><div class="col-sm-4"><label>Responsible</label></div><div class="col-sm-8"><span>' + data.name + ' (' + data.ppNumber + ')</span></div></div><div class="row col-md-12 mt-2"><div class="col-md-4"><label> Loan Case </label></div><div class="col-md-8"><input id="resp_loan_case" class="form-control" type="number" /></div></div><div class="row col-md-12 mt-2"><div class="col-md-4"><label> LC Amount </label></div><div class="col-md-8"><input id="resp_loan_amount" class="form-control" type="number" /></div></div><div class="row col-md-12 mt-2"><div class="col-md-4"><label> Account Number </label></div><div class="col-md-8"><input id="resp_account_number" class="form-control" type="number" /></div></div><div class="row col-md-12 mt-2"><div class="col-md-4"><label>ACC Amount </label></div><div class="col-md-8"><input id="resp_account_amount" class="form-control" type="number" /></div></div><div class="row col-md-12 mt-2"><div class="col-md-4"><label>Remarks/Reason</label></div><div class="col-md-8"><textarea id="resp_remarks" class="form-control" rows="3"></textarea></div></div></div>');
                    if (selectedRow) {
                        $('#resp_loan_case').val($(selectedRow).parent().parent().children('td').eq(3).html());
                        $('#resp_loan_amount').val($(selectedRow).parent().parent().children('td').eq(4).html());
                        $('#resp_account_number').val($(selectedRow).parent().parent().children('td').eq(5).html());
                        $('#resp_account_amount').val($(selectedRow).parent().parent().children('td').eq(6).html());
                        $('#resp_remarks').val('');
                    }
                } else {
                    $('#matchedPPNoPanels').append('<div class="row"><span>No record found..</span></div>');
                }
            }
        });
    }

    function saveResp(action) {
        if (!respUser.length || respUser[0].ppNumber <= 0) return;
        var lc = $('#resp_loan_case').val();
        var acc = $('#resp_account_number').val();
        if (lc === '' && acc === '') {
            alert('Please enter Either Loan Case Or Account Number to Proceed');
            return;
        }
        $.ajax({
            url: g_asiBaseURL + '/ApiCalls/add_responsible_to_observation',
            type: 'POST',
            data: {
                'PP_NO': respUser[0].ppNumber,
                'LOAN_CASE': $('#resp_loan_case').val(),
                'LC_AMOUNT': $('#resp_loan_amount').val(),
                'ACCOUNT_NUMBER': $('#resp_account_number').val(),
                'ACC_AMOUNT': $('#resp_account_amount').val(),
                'EMP_NAME': respUser[0].name,
                'REMARKS': $('#resp_remarks').val(),
                'NEW_PARA_ID': opts.newParaId,
                'OLD_PARA_ID': opts.oldParaId,
                'INDICATOR': opts.indicator,
                'COM_ID': opts.comId,
                'ACTION': action
            },
            dataType: 'json',
            success: function (data) {
                alert(data.Message);
                modal.modal('hide');
                load();
            }
        });
    }

    table.off('click', '.updateResp').on('click', '.updateResp', function (e) {
        e.preventDefault();
        selectedRow = this;
        modal.modal('show');
        $('#addResponsibleButton').addClass('d-none');
        $('#updateResponsibleButton').removeClass('d-none');
        $('#deleteResponsibleButton').removeClass('d-none');
        $('#matchedPPNoPanels').empty();
        $('#responsiblePPNoEntryField').val($(this).parent().parent().attr('id').split('tr_')[1]);
        getMatchedPP();
    });

    if (!opts.readOnly) {
        $('#addResponsibleButton').off('click').on('click', function () { saveResp('A'); });
        $('#updateResponsibleButton').off('click').on('click', function () { saveResp('U'); });
        $('#deleteResponsibleButton').off('click').on('click', function () { saveResp('D'); });
        $('#responsiblePPNoEntryField').off('keypress').on('keypress', function (e) { if (e.which === 13) { e.preventDefault(); getMatchedPP(); } });
        modal.on('show.bs.modal', function () {
            $('#matchedPPNoPanels').empty();
            selectedRow = null;
            respUser = [];
            $('#responsiblePPNoEntryField').val('');
            $('#addResponsibleButton').removeClass('d-none');
            $('#updateResponsibleButton').addClass('d-none');
            $('#deleteResponsibleButton').addClass('d-none');
        });
    }

    function updateContext(c) {
        opts = $.extend(opts, c || {});
        load();
    }

    load();

    return {
        reload: load,
        updateContext: updateContext,
        getMatchedPP: getMatchedPP,
        saveResp: saveResp
    };
}
