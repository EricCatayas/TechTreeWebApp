$(function () {

    function WireUpDatePicker()
    {
        var currDate = new Date();

        $('.datepicker').datepicker(
            {
                dateFormat: 'mm-dd-yy',
                minDate: currDate,
                maxDate: MaxdDateItemRelease(currDate, 2)
            });
    }
    WireUpDatePicker();
    // First js () actually
    
});