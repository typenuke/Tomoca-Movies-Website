function onReady()
{
    var qrcode = new QRCode("id_qrcode", {
        text:"tomoca.xyz",
        width:134,
        height:134,
        colorDark:"#000000",
        colorLight:"#ffffff",

        correctLevel:QRCode.CorrectLevel.H
    });
}
