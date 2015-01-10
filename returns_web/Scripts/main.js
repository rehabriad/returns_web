$('#myTab a').click(function(e) {
    e.preventDefault();
    $(this).tab('show');
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
