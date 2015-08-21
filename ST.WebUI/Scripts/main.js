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

    if (parseInt($("#nettaxpy").val(), 10) != parseInt($("#saleltc").val(), 10) - parseInt($("#purctdt2").val(), 10)) {
        if (confirm("قيمة صافى الضريبة لا تساوى الفرق بين قيمة المبيعات و قيمة المشتريات ..هل تريد الأستمرار فى الحفظ?")==false) {
            return false;
        }
    }
   

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
function CheckRetcode(data) {

    var retcode = data;
    if (retcode == 2) {
        $("#retSaleAdd").hide();
        $("#saleltcdiv").hide();
        $("#purctdt2div").hide();
        $("#nettaxdiv").hide();
        $("#salespurchtab").hide();

    }
    if (retcode == 1) {
        $("#retSaleAdd").show();
        $("#saleltc").show();
        $("#purctdt2").show();
        $("#nettaxpy").show();
        $("#salespurchtab").show();

    }
}
function PrintDocLocNum(data,rin,taxyrmo,transdate) {
   
    var mywindow = window.open('', 'my div', 'height=600,width=750');
    mywindow.document.write('<html><head><title>رقم الوثيقة</title>');
   
    /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
    mywindow.document.write('</head><body dir="rtl" ><div><img src=\"/images/banner.png\" alt=""  height="150" width="720" /></div><br><div style="width: 700px; color: navy; background-color: white; border: 2px solid blue; padding: 5px;"><h4> برجاء الإحتفاظ بهذا الرقم حيث أنه يمنكم الإستعلام عن إقراركم بهذا الرقم ');
    mywindow.document.write('<br>');

    mywindow.document.write('رقم التسجيل  :');
    mywindow.document.write(rin);

    mywindow.document.write('<br>');

    mywindow.document.write('الفترة الضريبية  :');
    mywindow.document.write(taxyrmo);

    mywindow.document.write('<br>');

    mywindow.document.write('تاريخ المعاملة  :');
    mywindow.document.write(transdate);

    mywindow.document.write('<br>');

    mywindow.document.write('رقم الوثيقة  :');
    mywindow.document.write(data);
    mywindow.document.write('</h4></div></body></html>');

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
    maxDate: 'Now',
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


$(document).ready(function () {

    $("#retSaleAdd").on("click", function () {
        var url = $(this).data('request-url');
        $.get(url, function (data) {
            $("#retSaleTbl tbody").append(data);

        });
    });
    $("#retPurchAdd").on("click", function () {
        var url = $(this).data('request-url');
        $.get(url, function (data) {
            $("#retPurchTbl tbody").append(data);

        });
    });

    $("#taxyrmo").datepicker({
        minDate: "-2M",
        maxDate: "-1M",
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        dateFormat: 'MM yy',
        onClose: function () {
            var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            var date = new Date(year, month, 1);
            console.log(date);
            $("#taxyrmo").datepicker('setDate', date);

        },
        beforeShow: function (input, inst) {
            setTimeout(function () {
                inst.dpDiv.css({
                    top: 495,
                    left: 720
                });
            }, 0);
        },
        onSelect: function () {
            $("#taxyrmo-check").addClass("hidden");
            var date1 = $(this).val();
            var url = $("#taxyrmo").data('request-url');
            $.post(url, { taxyrmo: date1 }, function (data) {
                console.log(data.toLowerCase());
                console.log(data.toLowerCase() == 'true');
                if (data.toLowerCase() == 'true') {
                    $("#taxyrmo-check").removeClass("hidden");
                }

            });

        }
    });
    nettaxpy
    $("#nettaxpy").click(function () {
        $("#nettaxpy").val(parseInt($("#saleltc").val(), 10) - parseInt($("#purctdt2").val(), 10));

        $("#nettaxpy").css({ "color": "green" });
    });
    jQuery("#nettaxpy").blur(function () {
        if (parseInt($("#nettaxpy").val(), 10) != parseInt($("#saleltc").val(), 10) - parseInt($("#purctdt2").val(), 10)) {
            $("#nettaxpy").css({ "color": "red" });

        }
    });

    jQuery("#saleltc").blur(function () {
        $("#nettaxpy").val(parseInt($("#saleltc").val(), 10) - parseInt($("#purctdt2").val(), 10));
        $("#nettaxpy").focus()

        $("#nettaxpy").css({ "color": "green" });

       
    });
    jQuery("#purctdt2").focusout(function () {
        $("#nettaxpy").val(parseInt($("#saleltc").val(), 10) - parseInt($("#purctdt2").val(), 10));
        $("#nettaxpy").focus()
        $("#nettaxpy").css({ "color": "green" });


    });
  
    $("#returncode").click(function () {
    
        var retcode = $("#returncode").val();
        if (retcode == 2){
            $("#retSaleAdd").hide();
            $("#saleltcdiv").hide();
            $("#purctdt2div").hide();
            $("#nettaxdiv").hide();
            $("#salespurchtab").hide();
            
        }
        if (retcode == 1) {
            $("#purctdt2div").show();
            $("#nettaxdiv").show();
            $("#salespurchtab").show();
            $("#retSaleAdd").show();
            $("#saleltcdiv").show();
          
            
        }
    });
   
});

//$("#taxyrmo").datepicker({
//    minDate: new Date(date.getYear(), date.getMonth() - 2, 1),
//    maxDate: new Date(date.getYear(), date.getMonth()-1, 0),
//    changeMonth: true,
//    changeYear: true,
//    showButtonPanel: true,
//    dateFormat: 'MM yy',
//    onClose: function () {
//        var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
//        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
//        var date = new Date(year, month, 1);
//        console.log(date);
//        $("#taxyrmo").datepicker('setDate', date);

//    },
//    beforeShow: function (input, inst) {
//        setTimeout(function () {
//            inst.dpDiv.css({
//                top: 100,
//                left: 600
//            });
//        }, 0);
//    }
//});


