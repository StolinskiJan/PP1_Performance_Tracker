// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict"
//==========================================

const titles = document.querySelectorAll('.accordion__title');
const contents = document.querySelectorAll('.accordion__content');



titles.forEach(item => item.addEventListener('click', () => {
    const activeContent = document.querySelector('#' + item.dataset.tab);

    if (!activeContent) return;

    if (activeContent.classList.contains('active')) {
        activeContent.classList.remove('active');
        item.classList.remove('active');
        activeContent.style.maxHeight = 0;
        activeContent.style.overflow = "hidden";
    } else {
        contents.forEach(element => {
            element.classList.remove('active');
            //element.style.maxHeight = 0;
            element.style.removeProperty('max-height');
            element.style.overflow = "hidden";
        });

        titles.forEach(element => element.classList.remove('active'));

        item.classList.add('active');
        activeContent.classList.add('active');
        activeContent.style.maxHeight = activeContent.scrollHeight + 'px';
        activeContent.style.overflow = "visible";
    }
}))

//document.querySelector('[data-tab="tab-3"]').classList.add('active');
//document.querySelector('#tab-3').classList.add('active');
//document.querySelector('#tab-3').style.maxHeigh = document.querySelector('#tab-3').scrollHeight + 'px';