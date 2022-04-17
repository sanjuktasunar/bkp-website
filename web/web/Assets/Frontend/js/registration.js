export function multiStepFormHandling(){
    const allStepForms = document.querySelectorAll('.step');
    const allPrevBtn = document.querySelectorAll('.prev-btn');
    const allNextBtn = document.querySelectorAll('.next-btn');
    
    const stepIndicatorIcon = document.querySelectorAll('.step-indicator-icon');
    const stepIndicatorText = document.querySelectorAll('.step-indicator-text');


    allNextBtn.forEach((value,index) => {
        allNextBtn[index].addEventListener('click',(e)=>{
            e.preventDefault();
            allStepForms[index].classList.add('hidden');
            allStepForms[index + 1].classList.remove('hidden');

            stepIndicatorIcon[index].innerHTML = '&#10003;';
            stepIndicatorText[index + 1].classList.add('font-semibold')
            stepIndicatorIcon[index + 1].style.backgroundColor = '#22c55e'

            if(index > 4){
                stepIndicatorIcon[6].innerHTML = '&#10003;'; 
            }

        })

    })

    allPrevBtn.forEach((value,index) => {
        
        allPrevBtn[index].addEventListener('click',(e)=>{
            e.preventDefault();

            allStepForms.forEach((form) => form.classList.add('hidden') )
            allStepForms[index].classList.remove('hidden');
            stepIndicatorIcon[index].innerHTML = index + 1;
            stepIndicatorText[index + 1].classList.remove('font-semibold')
            stepIndicatorIcon[index + 1].removeAttribute("style");

            if(index > 4){
                stepIndicatorIcon[6].innerHTML = index + 2;
            }
        })
    })
}