console.log("Hello World");


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