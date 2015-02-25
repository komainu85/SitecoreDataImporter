var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Speak = require("sitecore/shell/client/Speak/Assets/lib/core/1.2/SitecoreSpeak");

var Dataimporter = (function (_super) {
    __extends(Dataimporter, _super);
    function Dataimporter() {
        _super.apply(this, arguments);
    }
    Dataimporter.prototype.initialize = function () {
        alert("hello World");
    };
    return Dataimporter;
})(Speak.PageCode);
;

Sitecore.Speak.pageCode(["bootstrap", "scPipeline"], new Dataimporter());
