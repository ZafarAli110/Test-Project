//document.ready
$(function () {

    var ajaxFormSubmit = function () {
        var $form = $(this);  //grabbing the form 
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        // jQuery ajax with callback response (asynchoronous jquery call)
        $.ajax(options).done(function (responseData) {
            var $target = $($form.attr("data-otf-target"));  //where target = DOM element to be updated
            // $target.replaceWith(responseData);
            var $newHtml = (responseData); //var $newHtml = $(responseData)=>original but not working
            $target.replaceWith($newHtml);
            $newHtml.effect("highlight");  //jquery UI effect not working

        });

        return false;
    };


     // jquery UI autoComplete

    var submitAutoCompleteForm = function (event, ui) {  //from the jquery UI autocomplete widget document we shud pass
                                                         // event and UI as a function parameters
        $input = $(this); //grabbing the input element from DOM
        $input.val(ui.item.label); // setting its value

        var $form = $input.parents("form:first"); //finding the first 'form' of element
        $form.submit();  //submitting the founded form via jquery form submit method.

    };

    var createAutoComplete = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-otf-autocomplete"),
            select : submitAutoCompleteForm
        };
        $input.autocomplete(options);
    };

    var getPage = function () {
        var $a = $(this); //grabbing the anchor tag
        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (responseData) {
            var target = $a.parents("div.pagedList").attr("data-otf-target");
            $(target).replaceWith(responseData);
        });

        return false;

    };

   // $("form[data-otf-ajax]='true'").submit(ajaxFormSubmit);
    /*
    Lesson learned:
    Looks like we are removing form from DOM to be replaced by new form. In this case, we should delegate event.
    */
    $(document).on('submit', "form[data-otf-ajax='true']", ajaxFormSubmit); //always use this approach if possible 
    //$(document).each("input[data-otf-autocomplete]", createAutoComplete); //it will never work shud avoid it on inputs
    $("input[data-otf-autocomplete]").each(createAutoComplete);

    $(".main-content").on("click",".pagedList a",getPage);

});