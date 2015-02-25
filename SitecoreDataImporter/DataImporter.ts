import Speak = require("sitecore/shell/client/Speak/Assets/lib/core/1.2/SitecoreSpeak");

class Dataimporter extends Speak.PageCode {
    initialize() {
        alert("hello World");
    }
};

Sitecore.Speak.pageCode(["bootstrap", "scPipeline"], new Dataimporter());