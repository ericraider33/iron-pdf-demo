define([], function() 
{
   class MyModule
   {
       /**
        * @type {String}
        */
       name;
       
       constructor(name) 
       {
           this.name = name;
       }
       
       toString()
       {
           return this.name;
       }
   }    
   
   return MyModule;
});