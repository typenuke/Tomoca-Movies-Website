function onLoaderFunc()
{
  $(".seatStructure *").prop("disabled", true);
  $(".seats-inf *").prop("disabled", true);
}

function takeData()
{
  var allNumberVals = [];

  if ($("#Numseats").val().length == 0)
  {
  alert("Please Enter Number of Seats");
  }
  else
  {
    allNumberVals.push($("#Numseats").val());
    $("#NumberDisplay").text(allNumberVals);
    $(".inputForm *").prop("disabled", true);
    $(".seatStructure *").prop("disabled", false);
  }
}


function confirmTicket() { 
    
  if ($("input:checked").length == ($("#Numseats").val()))
    {
      $(".seatStructure *").prop("disabled", true);
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
