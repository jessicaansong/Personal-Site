// JavaScript source code
(function () {

    //============  Word Filter Reader =====================================//
    window.wordFilterReadText = function () {
        var reader = new FileReader();
        var output = "";
        var filePath = document.getElementById('wordFilterFileInput');
        if (filePath.files && filePath.files[0]) {
            reader.onload = function (e) {
                output = e.target.result;
                var wordFilter = wordFilterChecker(output);
                displayWordFilter(wordFilter)
            }; //end onload()
            reader.readAsText(filePath.files[0]);
        } //end if html5 filelist support
    }
    //============== Word Filter Checker & Display =======================//
    function wordFilterChecker(txt) {
        var userNumber = parseFloat(document.getElementById('userNumberWordFilter').value);
        var textArray = txt.split(new RegExp(/[\s,()\n\r\t]+/));
        var wordFilter = '';
        var selectedWord = '';
        var printedWords = {};
        for (i = 0; i < textArray.length; i++) {
            selectedWord = textArray[i].replace(/[^\w]/gi, ' ').toLowerCase();
            if (selectedWord.length > userNumber) {
                if (printedWords[selectedWord] == null) {
                    wordFilter += selectedWord + "<br>";
                    printedWords[selectedWord] = true;
                }
            }
        }
        return wordFilter;
    }
    function displayWordFilter(word) {
        var filterResult = document.getElementById('filterResult');
        //filterResult.innerHTML += word + "<br>";
        filterResult.innerHTML = word;
    }
})();