export function init() {
    console.log('执行gen.js');
    const dateContainer = document.getElementById('nowDate');
    dateContainer.textContent = formatNowDate();
}

function formatNowDate()
{
    const now = new Date();
    const year = now.getFullYear();
    const month = now.getMonth() + 1;
    const day = now.getDate();
    return `更新: ${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
}