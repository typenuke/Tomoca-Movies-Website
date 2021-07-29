$(document).ready(function() {
    var itemsMainDiv = ('.MultiCarousel');
    var itemsDiv = ('.MultiCarousel-inner');
    var itemWidth = "";

    $('.leftLst, .rightLst').click(function() {
        var condition = $(this).hasClass("leftLst");
        if (condition)
            click(0, this);
        else
            click(1, this)
    });

    ResCarouselSize();
    $(window).resize(function() {
        ResCarouselSize();
    });

    //this function define the size of the items
    function ResCarouselSize() {
        var incno = 0;
        var dataItems = ("data-items");
        var itemClass = ('.item');
        var id = 0;
        var btnParentSb = '';
        var itemsSplit = '';
        var sampwidth = $(itemsMainDiv).width();
        var bodyWidth = $('body').width();
        $(itemsDiv).each(function() {
            id = id + 1;
            var itemNumbers = $(this).find(itemClass).length;
            btnParentSb = $(this).parent().attr(dataItems);
            itemsSplit = btnParentSb.split(',');
            $(this).parent().attr("id", "MultiCarousel" + id);


            if (bodyWidth >= 1200) {
                incno = itemsSplit[3];
                itemWidth = sampwidth / incno;
            } else if (bodyWidth >= 992) {
                incno = itemsSplit[2];
                itemWidth = sampwidth / incno;
            } else if (bodyWidth >= 768) {
                incno = itemsSplit[1];
                itemWidth = sampwidth / incno;
            } else {
                incno = itemsSplit[0];
                itemWidth = sampwidth / incno;
            }
            $(this).css({ 'transform': 'translateX(0px)', 'width': itemWidth * itemNumbers });
            $(this).find(itemClass).each(function() {
                $(this).outerWidth(itemWidth);
            });

            $(".leftLst").addClass("over");
            $(".rightLst").removeClass("over");

        });
    }


    //this function used to move the items
    function ResCarousel(e, el, s) {
        var leftBtn = ('.leftLst');
        var rightBtn = ('.rightLst');
        var translateXval = '';
        var divStyle = $(el + ' ' + itemsDiv).css('transform');
        var values = divStyle.match(/-?[\d\.]+/g);
        var xds = Math.abs(values[4]);
        if (e == 0) {
            translateXval = parseInt(xds) - parseInt(itemWidth * s);
            $(el + ' ' + rightBtn).removeClass("over");

            if (translateXval <= itemWidth / 2) {
                translateXval = 0;
                $(el + ' ' + leftBtn).addClass("over");
            }
        } else if (e == 1) {
            var itemsCondition = $(el).find(itemsDiv).width() - $(el).width();
            translateXval = parseInt(xds) + parseInt(itemWidth * s);
            $(el + ' ' + leftBtn).removeClass("over");

            if (translateXval >= itemsCondition - itemWidth / 2) {
                translateXval = itemsCondition;
                $(el + ' ' + rightBtn).addClass("over");
            }
        }
        $(el + ' ' + itemsDiv).css('transform', 'translateX(' + -translateXval + 'px)');
    }

    //It is used to get some elements from btn
    function click(ell, ee) {
        var Parent = "#" + $(ee).parent().attr("id");
        var slide = $(Parent).attr("data-slide");
        ResCarousel(ell, Parent, slide);
    }

});

function myDropdown() {
    document.getElementById("myDropdown1").classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
function myFunctiondrop() {
    document.getElementById("myDropdown2").classList.toggle("show2");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function(event) {
    if (!event.target.matches('.dropbtnn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content2");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show2')) {
                openDropdown.classList.remove('show2');
            }
        }
    }
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("testtuong");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show2')) {
                openDropdown.classList.remove('show2');
            }
        }
    }
}

function baymavl() {
    document.getElementById("myDropdown11").classList.toggle("show3");
}


//Color change and scale K.H

$(".ticket").click(function () {
    $(".item-day").removeClass("clicked-day");
    $(".item-day-l").removeClass("clicked-day-l");
    $(this).find(".item-day").addClass("clicked-day");
    $(this).find(".item-day-l").addClass("clicked-day-l");
});

$(".item").click(function () {
    $(".pad15").removeClass("anchoape1");
    $(".pad151").removeClass("anchoape2");
    $(".pad152").removeClass("anchoape3");
    $(this).find(".pad15").addClass("anchoape1");
    $(this).find(".pad151").addClass("anchoape2");
    $(this).find(".pad152").addClass("anchoape3");
});

//Dropdown

//$('#mySelect').change(function () {
//    var value = $(this).val();
//});

$('#thanhpho').change(function () {
    var tp = $("#thanhpho option:selected").text();

    if (tp == "Hồ Chí Minh") {
        $('#cacrap').val('');
        $("#cacrap option[value='1']").css('display', 'block');
        $("#cacrap option[value='2']").css('display', 'block');
        $("#cacrap option[value='3']").css('display', 'block');
        $("#cacrap option[value='4']").css('display', 'block');
        $("#cacrap option[value='5']").css('display', 'block');
        $("#cacrap option[value='6']").css('display', 'none');
        $("#cacrap option[value='7']").css('display', 'none');
        $("#cacrap option[value='8']").css('display', 'none');
        $("#cacrap option[value='9']").css('display', 'none');
        $("#cacrap option[value='10']").css('display', 'none');
        $("#cacrap option[value='11']").css('display', 'none');
        $("#cacrap option[value='12']").css('display', 'none');
        $("#cacrap option[value='13']").css('display', 'none');
        $("#cacrap option[value='14']").css('display', 'none');
        $("#cacrap option[value='15']").css('display', 'none');
        $("#cacrap option[value='16']").css('display', 'none');
        $("#cacrap option[value='17']").css('display', 'none');
        $("#cacrap option[value='18']").css('display', 'none');
        $("#cacrap option[value='19']").css('display', 'none');
        $("#cacrap option[value='20']").css('display', 'none');
        $("#cacrap option[value='21']").css('display', 'none');
        $("#cacrap option[value='22']").css('display', 'none');

    }

    if (tp == "Cần Thơ") {
        $('#cacrap').val('');
        $("#cacrap option[value='1']").css('display', 'none');
        $("#cacrap option[value='2']").css('display', 'none');
        $("#cacrap option[value='3']").css('display', 'none');
        $("#cacrap option[value='4']").css('display', 'none');
        $("#cacrap option[value='5']").css('display', 'none');
        $("#cacrap option[value='6']").css('display', 'none');
        $("#cacrap option[value='7']").css('display', 'none');
        $("#cacrap option[value='8']").css('display', 'none');
        $("#cacrap option[value='9']").css('display', 'none');
        $("#cacrap option[value='10']").css('display', 'none');
        $("#cacrap option[value='11']").css('display', 'block');
        $("#cacrap option[value='12']").css('display', 'block');
        $("#cacrap option[value='13']").css('display', 'block');
        $("#cacrap option[value='14']").css('display', 'block');
        $("#cacrap option[value='15']").css('display', 'none');
        $("#cacrap option[value='16']").css('display', 'none');
        $("#cacrap option[value='17']").css('display', 'none');
        $("#cacrap option[value='18']").css('display', 'none');
        $("#cacrap option[value='19']").css('display', 'none');
        $("#cacrap option[value='20']").css('display', 'none');
        $("#cacrap option[value='21']").css('display', 'none');
        $("#cacrap option[value='22']").css('display', 'none');
    }

    if (tp == "Hà Nội") {
        $('#cacrap').val('');
        $("#cacrap option[value='1']").css('display', 'none');
        $("#cacrap option[value='2']").css('display', 'none');
        $("#cacrap option[value='3']").css('display', 'none');
        $("#cacrap option[value='4']").css('display', 'none');
        $("#cacrap option[value='5']").css('display', 'none');
        $("#cacrap option[value='6']").css('display', 'block');
        $("#cacrap option[value='7']").css('display', 'block');
        $("#cacrap option[value='8']").css('display', 'block');
        $("#cacrap option[value='9']").css('display', 'block');
        $("#cacrap option[value='10']").css('display', 'block');
        $("#cacrap option[value='11']").css('display', 'none');
        $("#cacrap option[value='12']").css('display', 'none');
        $("#cacrap option[value='13']").css('display', 'none');
        $("#cacrap option[value='14']").css('display', 'none');
        $("#cacrap option[value='15']").css('display', 'none');
        $("#cacrap option[value='16']").css('display', 'none');
        $("#cacrap option[value='17']").css('display', 'none');
        $("#cacrap option[value='18']").css('display', 'none');
        $("#cacrap option[value='19']").css('display', 'none');
        $("#cacrap option[value='20']").css('display', 'none');
        $("#cacrap option[value='21']").css('display', 'none');
        $("#cacrap option[value='22']").css('display', 'none');
    }

    if (tp == "Bình Dương") {
        $('#cacrap').val('');
        $("#cacrap option[value='1']").css('display', 'none');
        $("#cacrap option[value='2']").css('display', 'none');
        $("#cacrap option[value='3']").css('display', 'none');
        $("#cacrap option[value='4']").css('display', 'none');
        $("#cacrap option[value='5']").css('display', 'none');
        $("#cacrap option[value='6']").css('display', 'none');
        $("#cacrap option[value='7']").css('display', 'none');
        $("#cacrap option[value='8']").css('display', 'none');
        $("#cacrap option[value='9']").css('display', 'none');
        $("#cacrap option[value='10']").css('display', 'none');
        $("#cacrap option[value='11']").css('display', 'none');
        $("#cacrap option[value='12']").css('display', 'none');
        $("#cacrap option[value='13']").css('display', 'none');
        $("#cacrap option[value='14']").css('display', 'none');
        $("#cacrap option[value='15']").css('display', 'block');
        $("#cacrap option[value='16']").css('display', 'block');
        $("#cacrap option[value='17']").css('display', 'block');
        $("#cacrap option[value='18']").css('display', 'block');
        $("#cacrap option[value='19']").css('display', 'block');
        $("#cacrap option[value='20']").css('display', 'none');
        $("#cacrap option[value='21']").css('display', 'none');
        $("#cacrap option[value='22']").css('display', 'none');
    }

    if (tp == "Huế") {
        $('#cacrap').val('');
        $("#cacrap option[value='1']").css('display', 'none');
        $("#cacrap option[value='2']").css('display', 'none');
        $("#cacrap option[value='3']").css('display', 'none');
        $("#cacrap option[value='4']").css('display', 'none');
        $("#cacrap option[value='5']").css('display', 'none');
        $("#cacrap option[value='6']").css('display', 'none');
        $("#cacrap option[value='7']").css('display', 'none');
        $("#cacrap option[value='8']").css('display', 'none');
        $("#cacrap option[value='9']").css('display', 'none');
        $("#cacrap option[value='10']").css('display', 'none');
        $("#cacrap option[value='11']").css('display', 'none');
        $("#cacrap option[value='12']").css('display', 'none');
        $("#cacrap option[value='13']").css('display', 'none');
        $("#cacrap option[value='14']").css('display', 'none');
        $("#cacrap option[value='15']").css('display', 'none');
        $("#cacrap option[value='16']").css('display', 'none');
        $("#cacrap option[value='17']").css('display', 'none');
        $("#cacrap option[value='18']").css('display', 'none');
        $("#cacrap option[value='19']").css('display', 'none');
        $("#cacrap option[value='20']").css('display', 'block');
        $("#cacrap option[value='21']").css('display', 'block');
        $("#cacrap option[value='22']").css('display', 'block');

    }
});

$('#thanhpho').change(function () {
    //bo disable
    tam = $('#thanhpho').val();

    if (tam != '') {
        $("#cacrap").prop("disabled", false);
    };

    $("#choosing-ghe").prop("disabled", true);
});

$(".ticket").click(function () {
    var tam = $(this).find("#ngayto").text();
    $("#luungay").val(tam);

    $("#thanhpho").prop("disabled", false);
});
$('#cacrap').change(function () {
    var tp = $("#cacrap option:selected").text();
    $("#tenrap").val(tp);

    //bo disable
    tam = $('#cacrap').val();

    if (tam != '') {
        $("#choosing-ghe").prop("disabled", false);
    };
});

$(document).ready(function () {
    $(".item-daynow").addClass("clicked-day");
    $('#cacrap').val('');
    $('#thanhpho').val('');
    $("#cacrap").prop("disabled", true);
    $("#thanhpho").prop("disabled", true);
    $("#choosing-ghe").prop("disabled", true);
    $('#tenghe').prop("disabled", true);
});



$(".item").click(function () {
    var tam = $(this).find("#tieudephim").text();

    $("#tenphimtrongo").text(tam);

});


$(".item").click(function () {
    var tam = $(this).find("#theloaiphim").text();

    $("#theloaiphimtrongo").text(tam);

});

$(".item").click(function () {
    var tam = $(this).find("#thoigianphim").text();

    $("#thoigianphimtrongo").text(tam);

    $("#thoigiandechon").css("background", "#5C5C5C");

});

$(".item").click(function () {
    var src2 = $("img:first", this).attr("src");

    $("#hinhtrongo").attr("src", src2);
});

$(".item").click(function () {
    var tam = $(this).find("#thoigianotren").text();

    $("#thoigiandechon").text(tam);

});

$(".item").click(function () {
    var tam = $(this).find("#tenrapotren").text();

    $("#rapphimoduoi").text(tam);

});

$("#thoigiandechon").click(function () {
    var tam = $(this).text();

    $("#thoigianoduoi").text(tam);

});

$("#thoigiandechon").click(function () {
    $("#thoigiandechon").css("background", "#FF7300");

});
$(".item").click(function () {
    var tam = $(this).find("#malid").text();
    $("#idmal").val(tam);
});
//
$(".congvip").click(function () {
    var tam = $("#tongtien").val();
    $("#tongtientien").val(tam);

    var tam = $("#number").val();
    $("#chonghevip").val(tam);
    var tam = $("#number2").val();
    $("#chonghethuong").val(tam);
});
$(".truvip").click(function () {
    var tam = $("#tongtien").val();
    $("#tongtientien").val(tam);

    var tam = $("#number").val();
    $("#chonghevip").val(tam);
    var tam = $("#number2").val();
    $("#chonghethuong").val(tam);
});
//
$(".congthuong").click(function () {
    var tam = $("#tongtien").val();
    $("#tongtientien").val(tam);

    var tam = $("#number").val();
    $("#chonghevip").val(tam);
    var tam = $("#number2").val();
    $("#chonghethuong").val(tam);
});
$(".truthuong").click(function () {
    var tam = $("#tongtien").val();
    $("#tongtientien").val(tam);

    var tam = $("#number").val();
    $("#chonghevip").val(tam);
    var tam = $("#number2").val();
    $("#chonghethuong").val(tam);
});