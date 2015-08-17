list (function () {
 
    //============= Word Search Reader ======================================//
    window.wordsearchReadText = function () {
        var reader = new FileReader();
 
        var output = "";
        var filePath = document.getElementById('searchFileInput');
        if (filePath.files && filePath.files[0]) { //if there is no file, then null, otherwise continue
            reader.onload = function (e) {
                output = e.target.result; //whatever is from the file, then put it in the output.
                //console.log(output);
                var wordSearch = wordSearchChecker(output);
                displayWordSearch(wordSearch)
            }; //end onload()
            reader.readAsText(filePath.files[0]);
        } //end if html5 filelist support
    }
 
    //============= WordSearch Checker & Display ==========================//
    function wordSearchChecker(txt) { //function to search for a given word
        var userKeyWord = document.getElementById('userKeyWord').value; //userkeyword is the word that the user enters
        var wordSearch = '';//set another wordsearch "box" that is empty right now until we put something in it
        var array = txt.match(new RegExp(userKeyWord, 'gi'));//set up an array that says that the text must match the userkeyword, gi refers to g=global (look for it many times) and i=case insensitive. new RegExp (new regular expression --> look for this particular variable.
        if (array == null) { //if the array is empty return the following sentence "userkeyword does not appear in the file
 
            wordSearch =  "<h4>" + userKeyWord + "</h4> " + " does not appear in this file.";
        } else if (array.length == 1) { //if the userkeyword appears once, then return the following statement: userkeyword appears '1' time.  
            wordSearch = "<h4>" + userKeyWord + "</h4> " + " appears " + array.length + " time.";
            wordSearch += "<br>";//insert breaks for clarity and aesthetic purposes
            wordSearch += "<br>";
            wordSearch += txt.replace(new RegExp(userKeyWord, 'gi'), "<strong class='btn btn-warning'>" + userKeyWord + "</strong>"); //replace keywords with the highlighter, makes it easier to see.
        } else {
            wordSearch = "<h4>" + userKeyWord + "</h4> " + " appears " + array.length + " times.";
            wordSearch += "<br>";
            wordSearch += "<br>";
            wordSearch += txt.replace(new RegExp(userKeyWord, 'gi'), "<strong class='btn btn-warning'>" + userKeyWord + "</strong>");
        }
        return wordSearch;
    }
    function displayWordSearch(word) {
        var searchResult = document.getElementById('resultWordSearch');
        searchResult.innerHTML = word;
    }
})();