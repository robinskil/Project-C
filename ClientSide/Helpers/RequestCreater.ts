export class KeyValuePair{
    key : string;
    value : string;
    constructor(key : string , value : string){
        this.key = key;
        this.value = value;
    }
}

export function CreateRequest(url : string , bodyParameters : KeyValuePair[]) : Promise<Response>{
    return fetch(url , {
        method : "POST",
        headers: {
            'Accept': 'application/json, application/xml, text/plain, text/html, *.*',
        },
        body : CreateFormData(bodyParameters)
    });
}

function CreateFormData(keyValuePairs : KeyValuePair[]) : FormData{
    let dataForm = new FormData();
    for (let i = 0; i < keyValuePairs.length; i++) {
        dataForm.append(keyValuePairs[i].key , keyValuePairs[i].value);
    }
    return dataForm;
}