var jq$ = jQuery.noConflict();
jq$(function () {
    jq$('.webLink').tipsy({ gravity: 's', fade: true });
    jq$('#usrname').tipsy({ trigger: 'focus', gravity: 'e' });
    jq$('#pswd').tipsy({ trigger: 'focus', gravity: 'e', fade: true });
});

function showHint(str) {
    var xmlhttp;
    if (str.length < 2) {
        document.getElementById("lbN").innerHTML = "";
        return;
    }
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            document.getElementById("lbN").innerHTML = xmlhttp.responseText;
        }
    }
    xmlhttp.open("GET", "js/getName.ashx?q=" + str, true);
    xmlhttp.send();
}