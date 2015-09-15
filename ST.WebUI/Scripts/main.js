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
$(document).on("click", ".retcapital-del", function () {
    $(this).parents(".retCapitalRow").remove();

});
$(document).on("click", ".retreceit-del", function () {
    $(this).parents(".retReceitRow").remove();

});
$(document).on("click", ".retlocalpurch-del", function () {
    $(this).parents(".retLocalPurchRow").remove();

});
$(document).on("click", ".retexpurch-del", function () {
    $(this).parents(".retExPurchRow").remove();

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

    var capitalIds = 0;
    $(".retCapitalRow").each(function () {
        var retCapital = this;
        $(retCapital).find(".form-control").each(function () {
            var control = this;
            var id = $(control).data("str");
            id = id.replace("**", capitalIds);
            $(control).attr("id", id);
            $(control).attr("name", id);
        });
        capitalIds++;
    });

    var receitIds = 0;
    $(".retReceitRow").each(function () {
        var retReceit = this;
        $(retReceit).find(".form-control").each(function () {
            var control = this;
            var id = $(control).data("str");
            id = id.replace("**", receitIds);
            $(control).attr("id", id);
            $(control).attr("name", id);
        });
        receitIds++;
    });

    var localpurchIds = 0;
    $(".retLocalPurchRow").each(function () {
        var retLocalPurch = this;
        $(retLocalPurch).find(".form-control").each(function () {
            var control = this;
            var id = $(control).data("str");
            id = id.replace("**", localpurchIds);
            $(control).attr("id", id);
            $(control).attr("name", id);
        });
        localpurchIds++;
    });
    
    var expurchIds = 0;
    $(".retExPurchRow").each(function () {
        var retExPurch = this;
        $(retExPurch).find(".form-control").each(function () {
            var control = this;
            var id = $(control).data("str");
            id = id.replace("**", expurchIds);
            $(control).attr("id", id);
            $(control).attr("name", id);
        });
        expurchIds++;
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
    $("#saletax").click(function () {
        if ($("#taxrateid").val() == 14) {

            $("#saletax").val(parseInt($("#saleval").val(), 10) * 0.1);
            $("#saletax").css({ "color": "green" });
        }
    });

    $("#purchtax").click(function () {

        if ($("#taxrate").val() == 14) {
            $("#purchtax").val(parseInt($("#purchval").val(), 10) * 0.1);
            $("#purchtax").css({ "color": "green" });
        }
    });

    $('.recietdate').datepicker({ dateFormat: 'dd/mm/yy' });

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
    $("#retCapitalAdd").on("click", function () {
        var url = $(this).data('request-url');
        $.get(url, function (data) {
            $("#retCapitalTbl tbody").append(data);

        });
    });

    $("#retReceitAdd").on("click", function () {
        var url = $(this).data('request-url');
        $.get(url, function (data) {
            $("#retReceitTbl tbody").append(data);

        });
        
    });

    $("#retLocalPurchAdd").on("click", function () {
        var url = $(this).data('request-url');
        $.get(url, function (data) {
            $("#retLocalPurchTbl tbody").append(data);
            $('.recietdate').datepicker({ dateFormat: 'dd/mm/yy' });
        });
    });

    $("#retExPurchAdd").on("click", function () {
        var url = $(this).data('request-url');
        $.get(url, function (data) {
            $("#retExPurchTbl tbody").append(data);
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
    //nettaxpy
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


