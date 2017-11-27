(function useWordAtLeastNTimes() {
    $.validator.unobtrusive.adapters.add("usewordatleastntimes", ["word", "n"], function(options) {
        options.rules["usewordatleastntimes"] = options.params;
        options.messages["usewordatleastntimes"] = options.message;
    });

    $.validator.addMethod("usewordatleastntimes", function(value, element, params) {
        var n = parseInt(params.n, 10);
        var word = params.word;
        return isValid(value, word, n);
    });

    function isValid(value, word, n) {
        if (value == null) return false;
        var matches = value.match(new RegExp(word, "g"));
        return matches == null ? false : matches.length >= n;
    }
})();
