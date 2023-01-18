var Fs = require('fs');
const _Path = require('path');

var ReplaceWord = /<template>/g

function CCreateTemplates (Path) {
    // PRIVATE FIELDS
    var Self = this;
    var TemplatePath = 'GulpApp/files-template/';
    var IncludesFilePath = 'includes.pug';
    var ListFilePath = '!_list.struct';

    var ContentFilters = [
        (content, tmp) => content.replace(/\/\/- /g, ''),
        (content, tmp) => content.replace(/<template>/g, tmp.toLowerCase()),
        (content, tmp) => content.replace(/\/\/\//g, ''),
        (content, tmp) => content.replace(/<project>/g, _Path.basename(_Path.dirname(__dirname))),
        (content, tmp) => content.replace(/<class>/g, getSharpTemplateName(tmp)),
        //(content, tmp) => content.replace(/<model>/g, '@model ' + getSharpTemplateName(tmp)),
        (content, tmp) => content.replace(/<model>/g, '@model IPartialModel'),
    ];
    // /PRIVATE FIELDS
    //--------------------------------------------
    // PUBLIC FIELDS
    // /PUBLIC FIELDS
    //--------------------------------------------
    // INIT
    function init () {
        setVars();
        setEvents();
    }
    //...................................
    function setVars () {
        TemplatePath = TemplatePath;
        IncludesFilePath = Path + IncludesFilePath;
        ListFilePath = Path + ListFilePath;
    }
    //...................................
    function setEvents () {}
    //...................................
    // /INIT
    //--------------------------------------------
    // PUBLIC
    Self.Create = function () {
        var list = getTmpList();

        list.forEach(function (Name) {
            createTemplate(Name);
        });
    };
    //...................................
    // /PUBLIC
    //--------------------------------------------
    // EVENTS
    //...................................
    // /EVENTS
    //--------------------------------------------
    // PRIVATE
    function getTmpList () {
        var content = Fs.readFileSync(ListFilePath, 'utf8');
        var list = content.split("\r\n");
        var result = [];

        list.forEach(function (Item) {
            var item = Item.trim();
            if (item) result.push(item);
        });

        return result;
    }
    //...................................
    function createTemplate (Name) {
        var new_path = Path;

        copyDir(TemplatePath, new_path, function (content) {
            ContentFilters.forEach((filter) => content = filter(content, Name));
            return content
        }, function (file) {
            return filterFileName(file, Name);
        });
    }
    //...................................
    function copyDir(from, to, filter_content, filter_name) {
        let files = Fs.readdirSync(from);

        for (file of files) {
            let path = to + filter_name(file);
            if (Fs.existsSync(path)) continue;
            
            let old_path = from + file;
            let content = Fs.readFileSync(old_path, 'utf8');

            content = filter_content(content);

            Fs.writeFileSync(path, content);
        }
    }
    //...................................
    function filterContent(content, template_name) {
        content = content.replace(ReplaceWord, template_name.toLowerCase());
        content = content.replace(/\/\/- /g, '');

        return content;
    }
    //...................................
    function getSharpTemplateName(template) {
        tmp = '';
        template.split(/[-_]/g).forEach(function (word) {
            tmp += word[0].toUpperCase() + word.slice(1);
        });

        return tmp;
    }
    //...................................
    function filterSharpContent(content, template_name) {
        tmp = getSharpTemplateName(template_name);
        content = content.replace(ReplaceWord, tmp);
        content = content.replace(/\/\//g, '');
        
        return content;
    }
    //...................................
    function parseTemplateName(name) {
        let words = name.split('-');
        let result = '';

        for (let word of words) {
            result += word[0].toUpperCase() + word.slice(1);
        }

        return result;
    }
    //...................................
    function filterFileName(file_name, template_name) {
        let key = 'cshtml';
        let ext = file_name.replace('template.', '');
        ext = ext == key ? ext : key + '.' + ext;

        return '_' + parseTemplateName(template_name) + '.' + ext;
    }
    //...................................
    // /PRIVATE
    //--------------------------------------------
    // PROPERTIES
    /*
    Object.defineProperty(Ss, 'Test', {
            _Test = Value;
        },

        get: function () {
            return _Test;
        }
    });
    */
    //...................................
    // /PROPERTIES
    //--------------------------------------------
    init();
    //--------------------------------------------
}

module.exports = CCreateTemplates;
