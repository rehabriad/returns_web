$('#myTab a').click(function(e) {
    e.preventDefault();
    $(this).tab('show');
});

$('#tblOne tr').each(function () { });


$(document).on("click", ".retpurch-del", function () {
    $(this).parents(".retPurchRow").remove();
});
$(document).on("click", ".retsale-del", function () {
    $(this).parents(".retSaleRow").remove();
});

$("#frm-returns").on("submit",function(e) {
    var retIds = 0;
    $(".retSaleRow").each(function () {
        var retSale = this;
        $(retSale).find(".form-control").each(function () {
            var control = this;
            var id = $(control).data("str");
            id = id.replace("**", retIds);
            $(control).attr("id", id);
            $(control).attr("name", id);
        });
        retIds++;
    });

    var purchIds = 0;
    $(".retPurchRow").each(function () {
        var retPurch = this;
        $(retPurch).find(".form-control").each(function () {
            var control = this;
            var id = $(control).data("str");
            id = id.replace("**", purchIds);
            $(control).attr("id", id);
            $(control).attr("name", id);
        });
        purchIds++;
    });
    

});

function PrintDocLocNum(data) {
    var mywindow = window.open('', 'my div', 'height=400,width=600');
    mywindow.document.write('<html><head><title>رقم الوثيقة</title>');
    /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
    mywindow.document.write('</head><body dir="rtl" ><h4> رقم الوثيقة: ');
    mywindow.document.write(data);
    mywindow.document.write('</h4></body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10

    mywindow.print();
    mywindow.close();

    return true;
}

$('#transdate').datepicker({
    format: 'dd/MM/yyyy',
    startDate: '-3d',
    currentText: 'Now',
    autoSize: true,
    gotoCurrent: true,
    showAnim: 'blind',
    highlightWeek: true,
    beforeShow: function (input, inst) {
        setTimeout(function () {
            inst.dpDiv.css({
                top: 100,
                left: 200
            });
        }, 0);
    }
});


$("#taxyrmo").datepicker({
    changeMonth: true,
    changeYear: true,
    showButtonPanel: true,
    dateFormat: 'MM yy',
    onClose: function (dateText, inst) {
        var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
        var date = new Date(year, month, 1);
        console.log(date);
        $("#taxyrmo").datepicker('setDate', date);

    },
    beforeShow: function (input, inst) {
        setTimeout(function () {
            inst.dpDiv.css({
                top: 100,
                left: 600
            });
        }, 0);
    }
});


