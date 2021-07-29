function onReady()
{
    var qrcode = new QRCode("id_qrcode", {
        text: "demovailan-001-site1.itempurl.com/Tomoca/TicketShow/" + $("#layidticket").val(),
        width:134,
        height:134,
        colorDark:"#000000",
        colorLight:"#ffffff",

        correctLevel:QRCode.CorrectLevel.H
    });
}
window.document.onkeydown = function (e) {
    if (!e) {
        e = event;
    }
    if (e.keyCode == 27) {
        lightbox_close();
    }
}

function lightbox_open() {
    var lightBoxVideo = document.getElementById("VisaChipCardVideo");
    window.scrollTo(0, 0);
    document.getElementById('light').style.display = 'block';
    document.getElementById('fade').style.display = 'block';
    lightBoxVideo.play();
}

function lightbox_close() {
    var lightBoxVideo = document.getElementById("VisaChipCardVideo");
    document.getElementById('light').style.display = 'none';
    document.getElementById('fade').style.display = 'none';
    lightBoxVideo.pause();
}
