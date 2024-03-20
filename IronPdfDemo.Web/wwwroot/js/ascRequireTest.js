console.log("Hello World");

const requireOn = true;
if (requireOn)
{
    require.config(
        {
            baseUrl: '/js'
        });
    require(['myModule'], function(MyModule)
    {
        let x = new MyModule('Require Works Fine');
        let y = $('#require-test');
        y.html(x.toString());

        console.log('From Require');
        window.ironpdf?.notifyRender();
    });
}
else
{
    window.ironpdf?.notifyRender();   
}
