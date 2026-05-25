function isValidLength(datalength,maxlength) {
    if (datalength !=0 && datalength > maxlength)
        return false;
    return true;
}

function isValidData(data) {    
    if (data == "" || data == null || data == undefined)
        return false;
    return true;
}

function isValidObject(object) {
    if ($(object).length < 1 || $(object) == null || $(object) == undefined)
        return false;
    return true;
}

function validateMaxLengthTextData(object, maxlength, field) {
    var isValid = true;
    if (!isValidObject(object)) {
        isValid = false;
    }    
    var value = $(object).val();    
    if (!isValidLength(value.length,maxlength)) {
        addMaxLenInlineFieldErrorMessage(object, field, maxlength);
        isValid = false;
    }
    return isValid;
}

function validateRequiredTextData(object, field) {
    var isValid = true;
    if (!isValidObject(object)) {
        isValid = false;
    }
      
    if(!isValidData($(object).val())) {
        addInlineFieldErrorMessage(object);
        isValid = false;
    }
    return isValid;
}

function validateRequiredTextData(object,field) {
    var isValid = true;
    if (!isValidObject(object)) {
        isValid = false;
    }
     
    if (!isValidData($(object).val())) {
        addInlineFieldErrorMessage(object,field);
        isValid = false;
    }
    return isValid;
}

function getRequiredCheckboxErrorMessage(field) {
    return "<span class='custom-inline-field-error'>You have to check " + field + " checkbox."  + "</span>";
}

function getRequiredFieldErrorMessage(field) {
    return "<span class='custom-inline-field-error'>" + THE_FIELD + " " +  field + " " + IS_REQUIRED + "</span>";
}

function getMaxLenErrorMessage(field, maxlen) {
    return "<span class='custom-inline-field-error'>" + MAX_LEN_FOR_THE_FIELD + " " + field + " " + IS + " " +  maxlen + " " + CHARACTERS + ".</span>";
}

function addInlineFieldErrorMessage(object) {
    if (!isValidObject(object))
        return false;
    var fieldName = $(object).attr("name");
    if (isValidData(fieldName)) {
        $(object).parent().append(getRequiredFieldErrorMessage(fieldName));
    } else
        $(object).parent().append(getRequiredFieldErrorMessage(""));
}

function addInlineFieldErrorMessage(object,field) {
    if (!isValidObject(object))
        return false;
    $(object).parent().append(getRequiredFieldErrorMessage(field));
}

function addMaxLenInlineFieldErrorMessage(object, field, maxlen) {
    if (!isValidObject(object))
        return false;
    $(object).parent().append(getMaxLenErrorMessage(field, maxlen));
}

function removeInlineFieldErrorMessage(object) {
    if (!isValidObject(object))
        return false;
    $(object).find(".custom-inline-field-error").remove();
}

function resetValidation(parentObject) {
    return $(parentObject).find(".custom-inline-field-error").remove();
}

function getCustomErrorMessage(msg) {
    return "<span class='custom-inline-field-error'>" + msg + "</span>";
}

function addCustomErrorMessage(object, msg) {
    $(object).parent().append(getCustomErrorMessage(msg));
}

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
    return pattern.test(emailAddress);    
}


