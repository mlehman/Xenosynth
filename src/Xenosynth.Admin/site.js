

function toggleCheckBoxes(chkBox){
    var formElements = document.forms[0].elements;
	for(var i = 0; i < formElements.length; i++){
	    if(formElements[i].type == 'checkbox'){
			formElements[i].checked = chkBox.checked;
		}
	}

}
