export function init() {
    console.log('执行gen.js');
    
    const dateContainer = document.getElementById('nowDate');
    dateContainer.textContent = formatNowDate();

    const fadeElements = document.querySelectorAll('.fade-in');
    const fadeInObserver = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          entry.target.classList.add('show');
        }
      });
    }, { threshold: 0.1 });
    fadeElements.forEach(el => fadeInObserver.observe(el));
}

function formatNowDate()
{
    const now = new Date();
    const year = now.getFullYear();
    const month = now.getMonth() + 1;
    const day = now.getDate();
    return `更新: ${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
}