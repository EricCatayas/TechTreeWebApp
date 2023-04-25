function MaxdDateItemRelease(date, numMonths) {

    var month = date.getMonth(date);
    ////Why store as a millisecond? January 1st, 1970 = 00:00:00 utc
    var milliseconds = new Date().setMonth(month + numMonths);

    return new Date(milliseconds);
}