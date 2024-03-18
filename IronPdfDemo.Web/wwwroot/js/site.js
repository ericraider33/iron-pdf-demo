$(document).ready(function ()
{
    var div = $('#js-test');
    div.html('Wow, JS actually works. Time is 0 seconds. window.status=' + window.status);

    var counter = 0;
    window.setInterval(function()
    {
        counter++;

        // Loads at the 3 second mark
        if (counter >=3 && window.status !== "loaded")
        {
            window.status = "loaded";
            window.ironpdf?.notifyRender();
        }
        
        div.html('Wow, JS actually works. Time is '+counter+' seconds. window.status=' + window.status);
    }, 1000);
});
