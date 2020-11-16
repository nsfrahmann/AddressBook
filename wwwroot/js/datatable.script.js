(function ($) {
    "use strict";
var editor;
 $('#example').DataTable({
     dom:
         
         "<'row align-items-center'<'col-sm-4'B><'col-sm-3 pt-2 before-me'l><'col-sm-3 pt-2'f>>" +
         "<'row mt-3'<'col-sm-12't>>" +
         "<'row'<'col-sm-6'i><'col-sm-6 mt-2'p>>",
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ],
     responsive: true,
     lengthChange: [10, 25, 50],
     lengthMenu: [10, 25, 50],
     language: {
         lengthMenu: "# of Tickets _MENU_"
     }
 });
 
    $(".before-me").before($(".test"));

})(jQuery);
