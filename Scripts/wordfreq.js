// JavaScript source code

//Word Freq//

(function () {

    //============  Word Frequency Reader =====================================//
    window.wordFrequencyReadText = function () {
        var reader = new FileReader();
        var output = "";
        var filePath = document.getElementById('freqFileInput2');
        if (filePath.files && filePath.files[0]) {
            reader.onload = function (e) {
                output = e.target.result;
                var wordFrequency = wordFrequencyChecker(output);
                displayWordFrequency(wordFrequency)
            }; //end onload()
            reader.readAsText(filePath.files[0]);
        } //end if html5 filelist support
    }
    //============== Word frequency Checker & Display =======================//
    function wordFrequencyChecker(txt) {
        var textArray = txt.split(" "); //separate by white space
        var wordFrequency = "";
        var selectedWord = "";
        var printedWords = {};
        // First Count The Words in the Array
        for (i = 0; i < textArray.length; i++) {
            selectedWord = textArray[i].replace(/[^\w]/gi, '').toLowerCase();
            if (printedWords[selectedWord] != null)
                printedWords[selectedWord]++;
            else
                printedWords[selectedWord] = 1;
        }

        // Create Dictionary - Create Array of Objs 
        var arr = [];
        for (key in printedWords) {
            arr.push({ word: key, count: printedWords[key] });
        }
        //Sort Dictionary in Descending Order
        arr.sort(function (a, b) { return b.count - a.count });

        wordFrequency = arr.reduce(function (a, b) {
            return a += b.word + " appears " + b.count + " times." + "<br>"
        }, " ");
        return wordFrequency;
    }
    function displayWordFrequency(word) {
        var freqResult = document.getElementById('freqResult');
        freqResult.innerHTML = word;
    }
})();