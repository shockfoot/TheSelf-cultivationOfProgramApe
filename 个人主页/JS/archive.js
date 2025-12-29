export function init() {
  console.log('执行archive.js');
    
  const sideElements = document.querySelectorAll('.side-in');
  showAllSideIn();
  function showAllSideIn() {
    let index = 0;
    const timer = setInterval(() => {
      if (index < sideElements.length) {
        sideElements[index].classList.add('show');
        index++;
      } else {
        clearInterval(timer);
      }
    }, 100);
  }

  const activeTagClass = 'activeArchiveTag';
  const tagContainer = document.getElementById('archiveTagContainer');
  const tagList = tagContainer.querySelectorAll('.archiveRoundTag');
  const allTag = new Set();
  tagList.forEach(tag => {
    tag.addEventListener('click', handleTagClick);
    if (tag.textContent != '全部') allTag.add(tag.textContent);
  });
  const showTagSet = new Set();
  function handleTagClick(e) {
    e.preventDefault();
    if (this.textContent == '全部') {
      if (this.classList.contains(activeTagClass)) return;
      console.log(this.textContent);
      showTagSet.clear();
      updateTagState();
      updateArticleList();
      return;
    }

    console.log(this.textContent);
    if (this.classList.contains(activeTagClass)) {
      showTagSet.delete(this.textContent);
      updateTagState();
      updateArticleList();
      return;
    }

    showTagSet.add(this.textContent);
    updateTagState();
    updateArticleList();
  }

  function updateTagState() {
    const n = tagList.length;
    if (showTagSet.size == 0) {
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
      if (element.classList.contains(activeTagClass)) {
        if (!showTagSet.has(element.textContent)) element.classList.remove(activeTagClass);
      }
      else if (showTagSet.has(element.textContent)) {
        element.classList.add(activeTagClass);
      }
    }
  }

  let isLoading = false;
  const articleList = document.querySelectorAll('.article-item');
  const articleCatalogueContiner = document.getElementById('articleCatalogueContiner');
  const articleContentContiner = document.getElementById('articleContentContiner');
  const contentContainer = document.getElementById('articleContent');
  articleList.forEach(e => e.addEventListener('click', handleArticleLinkClick));
  async function handleArticleLinkClick(e) {
    e.preventDefault();
    if (isLoading) return;
    loadHTMLContent(this.dataset.src);
  }
  async function loadHTMLContent(filePath) {
    isLoading = true;
    console.log(filePath);
    try {
      const response = await fetch(filePath);
      if (!response.ok) throw new Error(`请求失败: ${response.status}`);
      const htmlContent = await response.text();
      const parser = new DOMParser();
      const htmlDoc = parser.parseFromString(htmlContent, 'text/html');
      //const container = htmlDoc.getElementById(contentID);
      //if (!container) throw new Error(`未找到内容: ${contentID}`);
      contentContainer.innerHTML = htmlDoc.body.innerHTML;//container.innerHTML;
      articleCatalogueContiner.classList.add('hide');
      articleContentContiner.classList.add('show');
    }
    catch (error) {
      console.error(`加载${filePath}失败: ${error}`);
      contentContainer.innerHTML = `<div>加载失败: ${error.message}</div>`;
    }
    finally {
      isLoading = false;
    }
  }
  const returnA = document.getElementById('return');
  returnA.addEventListener('click', handleReturnClick);
  async function handleReturnClick(e) {
    e.preventDefault();
    if (isLoading) return;
    isLoading = true;
    articleCatalogueContiner.classList.remove('hide');
    articleContentContiner.classList.remove('show');
    await new Promise(resolve => setTimeout(resolve, 300));
    contentContainer.innerHTML = '';
    isLoading = false;
  }

  const articleCount = document.getElementById('articleCount');
  articleCount.textContent = articleList.length.toString();
  async function updateArticleList()
  {
    const sideList = new Set();
    const n = articleList.length;
    for (let i = 0; i < n; i++) {
      articleList[i].classList.add('article-item-hidden');
    }
    sideElements.forEach(e => {
      e.classList.remove('show');
    });
    await new Promise(resolve => setTimeout(resolve, 100));
    const tempTag = new Set();
    let count = 0;
    for (let i = 0; i < n; i++) {
      const tags = articleList[i].querySelectorAll('.archiveRoundTag');
      tempTag.clear();
      tags.forEach(t => tempTag.add(t.textContent));

      let active = true;
      showTagSet.forEach(t => {
        if (!tempTag.has(t)) active = false;
      });
      if (active) {
        articleList[i].classList.remove('article-item-hidden');
        const sideInItem = articleList[i].querySelectorAll('.side-in');
        sideInItem.forEach(e => sideList.add(e));
        count++;
      }
    }
    articleCount.textContent = count.toString();
    let index = 0;
    const list = Array.from(sideList);
    const timer = setInterval(() => {
      if (index < list.length) {
        list[index].classList.add('show');
        index++;
      } else {
        clearInterval(timer);
      }
    }, 100);
  }
}