export function init() {
  console.log('执行archive.js');
    
  const sideElements = document.querySelectorAll('.side-in');
  const sideInObserver = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        entry.target.classList.add('show');
      }
    });
  }, { threshold: 0.1 });
  sideElements.forEach(el => sideInObserver.observe(el));

  const activeTagClass = 'activeArchiveTag';
  const tagContainer = document.getElementById('archiveTagContainer');
  const tagList = tagContainer.querySelectorAll('.archiveRoundTag');
  const allTag = new Set();
  tagList.forEach(tag => {
    tag.addEventListener('click', handleTagClick);
    if (tag.textContent != '全部') allTag.add(tag.textContent);
  });
  const showTagSet = new Set(allTag);
  function handleTagClick(e) {
    e.preventDefault();
    if (this.textContent == '全部') {
      if (this.classList.contains(activeTagClass)) return;
      console.log(this.textContent);
      showTagSet.clear();
      allTag.forEach(v => showTagSet.add(v));
      updateTagState();
      updateArticleList();
      return;
    }

    console.log(this.textContent);
    if (this.classList.contains(activeTagClass)) {
      showTagSet.delete(this.textContent);
      if (showTagSet.size <= 0) {
        showTagSet.clear();
        allTag.forEach(v => showTagSet.add(v));
      }
      updateTagState();
      updateArticleList();
      return;
    }

    if (showTagSet.size == allTag.size) {
      showTagSet.clear();
    }
    showTagSet.add(this.textContent);
    updateTagState();
    updateArticleList();
  }

  function updateTagState() {
    const n = tagList.length;
    if (showTagSet.size == allTag.size) {
      for (let i = 0; i < n; i++) {
        const element = tagList[i];
        if (element.textContent == '全部') {
          if (!element.classList.contains(activeTagClass)) element.classList.add(activeTagClass);
          continue;
        }
        element.classList.remove(activeTagClass);
      }
      return;
    }

    for (let i = 0; i < n; i++) {
      const element = tagList[i];
      if (element.textContent == '全部') {
        element.classList.remove(activeTagClass);
        continue;
      }
      if (!element.classList.contains(activeTagClass) && showTagSet.has(element.textContent)) element.classList.add(activeTagClass);
    }
  }

  const articleList = document.querySelectorAll('.article-item')
  function updateArticleList()
  {
    const sideList = new Set();
    const n = articleList.length;
    if (showTagSet.size == allTag.size) {
      for (let i = 0; i < n; i++) {
        articleList[i].style.display = '';
        const sideInItem = articleList[i].querySelectorAll('.side-in');
        sideInItem.forEach(e => {
          e.classList.add('no-animation');
          e.classList.remove('show');
          sideList.add(e);
        });
      }
    } else {
      for (let i = 0; i < n; i++) {
        const tags = articleList[i].querySelectorAll('.archiveRoundTag');
        let active = true;
        showTagSet.forEach(t => {
          let hasT = true;
          tags.forEach(item => {
            if (item.textContent != t) hasT = false;
          });
          if (hasT == false) active = false;
        });
        if (active) {
          articleList[i].style.display = '';
          const sideInItem = articleList[i].querySelectorAll('.side-in');
          sideInItem.forEach(e => {
            e.classList.add('no-animation');
            e.classList.remove('show');
            sideList.add(e);
          });
        } else {
          articleList[i].classList.remove('show');
          articleList[i].style.display = 'none';
        }
      }
    }
    
    let index = 0;
    const list = Array.from(sideList);
    const timer = setInterval(() => {
      if (index < list.length) {
        list[index].classList.remove('no-animation');
        list[index].classList.add('show');
        index++;
      } else {
        clearInterval(timer);
      }
    }, 200);
  }
}