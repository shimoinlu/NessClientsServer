(function(){

    const is_israeli_id_number= function(id) {
        id = String(id).trim();
        if (id.length > 9 || isNaN(id)) return false;
        id = id.length < 9 ? ("00000000" + id).slice(-9) : id;
            return Array.from(id, Number).reduce((counter, digit, i) => {
                const step = digit * ((i % 2) + 1);
                return counter + (step > 9 ? step - 9 : step);
            }) % 10 === 0;
    }
    
    const trimInputs = function(form){

        [...form.getElementsByTagName("input")].map(input => input.value = input.value.trim())
    };
    const ValidateOneInput = function(i){
        if(i.valid){
            return true
        }
        else{
            i.reportValidity();
            return false;    
        }
    }
    const ValidateAllInputs = function(){
        const allInputs = [...form.getElementsByTagName("input")]
        return allInputs.every(ValidateOneInput);
    }
    document.addEventListener('DOMContentLoaded', () => {
        let f = document.getElementById("formNewClientDetails");
        let buttonSubmit = document.getElementById("buttonSubmit");
        buttonSubmit.addEventListener('click',(e)=>{
            trimInputs(f);
            let id = document.getElementById('id')
            if(!is_israeli_id_number(id.value) && ValidateAllInputs())
            {
                id.setCustomValidity("Bad check digit");
                id.reportValidity();
            }
            else
                f.submit();
            })
        })

    })
();





// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
console.log("shimon won")