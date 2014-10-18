function flashWrite(url, w, h, id, bg, vars, win, me) {
    var flashStr =
	"<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0' width='" + w + "' height='" + h + "' id='" + id + "' align='middle'>" +
	"<param name='allowScriptAccess' value='always' />" +
	"<param name='movie' value='" + url + "' />" +
	"<param name='FlashVars' value='" + vars + "' />" +
	"<param name='wmode' value='" + win + "' />" +
	"<param name='loop' value='true' />" +
	"<param name='allowfullscreen' value='true' />" +
	"<param name='quality' value='high' />" +
	"<param name='bgcolor' value='" + bg + "' />" +
	"<param name='menu' value='" + me + "' />" +
	"<embed src='" + url + "' FlashVars='" + vars + "' wmode='" + win + "' menu='" + me + "' loop='true' quality='high' bgcolor='" + bg + "' width='" + w + "' height='" + h + "' name='" + id + "' align='middle' allowScriptAccess='always'  allowfullscreen='true' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' />" +
	"</object>";
    document.write(flashStr);
}

function getFlashName() {
    var fileName = "./Assets/scene";

    var fileNum = Math.floor(Math.random() * (3 - 1 + 1)) + 1;

    return fileName + fileNum + ".swf"
}