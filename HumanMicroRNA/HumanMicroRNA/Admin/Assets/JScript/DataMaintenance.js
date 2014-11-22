var divName;
/*-------------------------------------------------------------------------
The functions below are designed for the download Data process.
Function 'function DownloadData(popup)' is designed to call a webmethod in
order to download the data from different sources.
Function 'onDownloadDataComplete(result, userContext, methodName)' is 
designed to be executed when the DownloadData is finished its execution
successfully.
Function 'onDownloadDataError(result, userContext, methodName)' is designed
to be executed when the DownloadData is finished its execution
unsuccessfully.
-------------------------------------------------------------------------*/
function DownloadData(popup) {
    divName = popup;
    ShowModalPopup(popup);

    PageMethods.DownloadData(onDownloadDataComplete, onDownloadDataError);
}
function onDownloadDataComplete(result, userContext, methodName) {
    HideModalPopup(divName);
}
function onDownloadDataError(result, userContext, methodName) {

}
/*-------------------------------------------------------------------------
The functions below are designed for the Update Data process.
Function 'function UpdateMiRNA(popup)' is designed to call a webmethod in
    order to update the data after it has been downloaded.
Function 'onUpdateDataComplete(result, userContext, methodName)' is 
    designed to be executed when the UpdateMiRNA is finished its execution
    successfully.
Function 'onUpdateDataError(result, userContext, methodName)' is designed
    to be executed when the UpdateMiRNA is finished its execution
    unsuccessfully.
-------------------------------------------------------------------------*/
function UpdateMiRNA(popup) {
    divName = popup;
    ShowModalPopup(popup);

    PageMethods.UpdateMiRNAData(onUpdateDataComplete, onUpdateDataError);
}
function onUpdateDataComplete(result, userContext, methodName) {
    HideModalPopup(divName);
}
function onUpdateDataError(result, userContext, methodName) {

}