function onLoaderFunc()
{
  $(".seatStructure *").prop("disabled", true);
  $(".seats-inf *").prop("disabled", true);
  $(".displayerBoxes *").prop("disabled", true);
  $("#number2").prop("disabled", true);
  $("#number").prop("disabled", true);
}

function takeData()
{
  var allNumberVals = [];
  var num = parseInt(document.getElementById('Numseats').value, 10);

  if ($("#Numseats").val().length == 0 || num == 0)
  {
  alert("Please Enter Number of Seats");
  }
  else
  {
    allNumberVals.push($("#Numseats").val());
    $("#NumberDisplay").text(allNumberVals);
    $(".inputForm *").prop("disabled", true);
    $(".seatStructure *").prop("disabled", false);
    $(".value-button").hide();
    $("#number").css("margin-left", "63%");
    $("#number2").css("margin-left", "63%");
  }
}



function confirmTicket() { 
    
  if ($("input:checked").length == ($("#Numseats").val()))
    {
      $(".seatStructure *").prop("disabled", true);
      var allSeatsVals = [];

      //Storing in Array
      
      $('#seatsBlock :checked').each(function() {
        allSeatsVals.push($(this).val());
      });
      //Displaying
      $('#seatsDisplay').val(allSeatsVals);
    }
  else
    {
      alert("Please select " + ($("#Numseats").val()) + " seats")
    }
  }

$(":checkbox").click(function() {
  if ($("input:checked").length == ($("#Numseats").val())) {
    $(":checkbox").prop('disabled', true);
    $(':checked').prop('disabled', false);
  }
  else
    {
      $(":checkbox").prop('disabled', false);
    }
  });

// Button VIP code

function increaseValue() {
  var value = parseInt(document.getElementById('number').value, 10);
  value = isNaN(value) ? 0 : value;
  value++;
  value > 10 ? value = 10 : '';
  document.getElementById('number').value = value;
}

function decreaseValue() {
  var value = parseInt(document.getElementById('number').value, 10);
  value = isNaN(value) ? 0 : value;
  value < 1 ? value = 1 : '';
  value--;
  document.getElementById('number').value = value;
}

// Button Normal code

function increaseValue2() {
  var value = parseInt(document.getElementById('number2').value, 10);
  value = isNaN(value) ? 0 : value;
  value++;
  value > 10 ? value = 10 : '';
  document.getElementById('number2').value = value;
}

function decreaseValue2() {
  var value = parseInt(document.getElementById('number2').value, 10);

  value = isNaN(value) ? 0 : value;
  value < 1 ? value = 1 : '';
  value--;
  document.getElementById('number2').value = value;
}

//Amount code

//Cong vip
var checkcongvip = 1;
function vipcong() {
  var v1 = parseInt(document.getElementById('number').value, 10);
  var tongtien = parseInt(document.getElementById('tongtien').value, 10);
  var num = parseInt(document.getElementById('Numseats').value, 10);

  checktruvip = 1;

  if(checkcongvip==1){
    tongtien = tongtien + 150000;
    $('#tongtien').val(tongtien);

    num++;
    $('#Numseats').val(num);
  }
  if(v1==10){
    checkcongvip = 2;
  }
}

//Tru vip
var checktruvip = 1;
function viptru() {
  var v1 = parseInt(document.getElementById('number').value, 10);
  var tongtien = parseInt(document.getElementById('tongtien').value, 10);
  var num = parseInt(document.getElementById('Numseats').value, 10);
  
  checkcongvip = 1;


  if(checktruvip==1){
    tongtien = tongtien - 150000;
    $('#tongtien').val(tongtien);

    num--;
    $('#Numseats').val(num);
  }
  if(v1==0){
    checktruvip = 2;
  }
  if(tongtien < 0){
    tongtien = 0;
    $('#tongtien').val(tongtien);
    num = 0;
    $('#Numseats').val(num);
  }
}

//Cong thuong
var checkcongthuong = 1;
function thuongcong() {
  var v1 = parseInt(document.getElementById('number2').value, 10);
  var tongtien = parseInt(document.getElementById('tongtien').value, 10);
  var num = parseInt(document.getElementById('Numseats').value, 10);

  checktruthuong = 1;
 
  if(checkcongthuong==1){
    tongtien = tongtien + 100000;
    $('#tongtien').val(tongtien);

    num++;
    $('#Numseats').val(num);
  }
  if(v1==10){
    checkcongthuong = 2;
  }
}


//Tru thuong
var checktruthuong = 1;
function thuongtru() {
  var v1 = parseInt(document.getElementById('number2').value, 10);
  var tongtien = parseInt(document.getElementById('tongtien').value, 10);
  var num = parseInt(document.getElementById('Numseats').value, 10);
  
  checkcongthuong = 1;


  if(checktruthuong==1){
    tongtien = tongtien - 100000;
    $('#tongtien').val(tongtien);

    num--;
    $('#Numseats').val(num);
  }
  if(v1==0){
    checktruthuong = 2;
  }
  if(tongtien < 0){
    tongtien = 0;
    $('#tongtien').val(tongtien);
    num = 0;
    $('#Numseats').val(num);
  }
}
