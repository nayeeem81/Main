
let selectedPanel = null;
const panelOrder = [];
var urlDeletePanel = '';
var urlUpdatePanelPositon = '';

// DOM ready
document.addEventListener('DOMContentLoaded', async () =>
{
    const listPanel = document.querySelector('.list-panel');

    function deleteElementById(elementId) {
        if (selectedPanel) {
            selectedPanel = null;
        }

        const element = document.getElementById(elementId);

        if (element) {
            element.remove();
        }
    }

    function removeArrows() {
        const arrowContainer = selectedPanel.querySelector('.arrow-buttons');

        if (arrowContainer)
            arrowContainer.remove();
    }

    function updateArrowVisibility() {
        if (!selectedPanel)
            return;

        const panels = Array.from(listPanel.querySelectorAll('.panel'));
        const currentIndex = panels.indexOf(selectedPanel);
        const isFirst = currentIndex === 0;
        const isLast = currentIndex === panels.length - 1;

        const upArrowBtn = selectedPanel.querySelector('.up-arrow-btn');
        const downArrowBtn = selectedPanel.querySelector('.down-arrow-btn');

        // Hide/disable up arrow if first
        if (upArrowBtn) {
            if (isFirst) {
                upArrowBtn.classList.add('disabled');
                upArrowBtn.disabled = true;
            }
            else {
                upArrowBtn.classList.remove('disabled');
                upArrowBtn.disabled = false;
            }
        }

        // Hide/disable down arrow if last
        if (downArrowBtn) {
            if (isLast) {
                downArrowBtn.classList.add('disabled');
                downArrowBtn.disabled = true;
            }
            else {
                downArrowBtn.classList.remove('disabled');
                downArrowBtn.disabled = false;
            }
        }
    }

    function scrollToMiddle()
    {
        if (!selectedPanel)
            return;

        selectedPanel.scrollIntoView({
            behavior: 'smooth',
            block: 'center'
        });
    }

    async function getValidationToken()
    {
        const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');

        if (!tokenElement)
        {
            console.log("Anti-forgery token element not found in the DOM.");
            return;
        }

        return tokenElement.value;
    }

    // Move selected panel up one step
    async function moveUp() {
        if (!selectedPanel)
            return;

        const panels = Array.from(listPanel.querySelectorAll('.panel'));
        const currentIndex = panels.indexOf(selectedPanel);

        if (currentIndex > 0) {
            const prevPanel = selectedPanel.previousElementSibling;
            listPanel.insertBefore(selectedPanel, prevPanel);
            await setPanelOrder();
            updateArrowVisibility();
            scrollToMiddle();
        }
    }

    // Move selected panel down one step
    async function moveDown() {
        if (!selectedPanel)
            return;

        const panels = Array.from(listPanel.querySelectorAll('.panel'));
        const currentIndex = panels.indexOf(selectedPanel);

        if (currentIndex < panels.length - 1) {
            const nextPanel = selectedPanel.nextElementSibling;
            listPanel.insertBefore(nextPanel, selectedPanel);
            await setPanelOrder();
            updateArrowVisibility();
            scrollToMiddle();
        }
    }

    async function addArrows() {
        const existingArrows = selectedPanel.querySelector('.arrow-buttons');

        if (existingArrows)
            return;

        const arrowContainer = document.createElement('div');
        arrowContainer.className = 'arrow-buttons';

        // Button
        // Up arrow button
        const upArrowBtn = document.createElement('button');
        upArrowBtn.className = 'arrow-btn up-arrow-btn';
        upArrowBtn.innerHTML = '↑';
        upArrowBtn.title = 'Move up';
        upArrowBtn.setAttribute('aria-label', 'Move up');

        upArrowBtn.addEventListener('click', async (e) => {
            e.stopPropagation();
            await moveUp();
        });

        // Button 
        // Down arrow button
        const downArrowBtn = document.createElement('button');
        downArrowBtn.className = 'arrow-btn down-arrow-btn';
        downArrowBtn.innerHTML = '↓';
        downArrowBtn.title = 'Move down';
        downArrowBtn.setAttribute('aria-label', 'Move down');

        downArrowBtn.addEventListener('click', async (e) => {
            e.stopPropagation();
            await moveDown();
        });

        // Button
        // Save button
        const saveBtn = document.createElement('button');
        saveBtn.className = 'save-btn';
        saveBtn.innerHTML = 'Save';
        saveBtn.title = 'Save or Press Enter';
        saveBtn.setAttribute('aria-label', 'Save or Press Enter');

        saveBtn.addEventListener('click', async (e) => {
            e.stopPropagation();
            await savePanelOrders();
            await unselectPanel();
        });

        // Button
        // Delete button
        const deleteBtn = document.createElement('button');
        deleteBtn.className = 'delete-btn';
        deleteBtn.innerHTML = 'Delete';
        deleteBtn.title = 'Click to Delete Panel';
        deleteBtn.setAttribute('aria-label', 'Click to Delete Panel');

        deleteBtn.addEventListener('click', async (e) => {
            e.stopPropagation();
            const panelId = selectedPanel.dataset.panelId;
            const pageId = selectedPanel.dataset.pageId;
            await deletePanel(panelId, pageId);
        });

        arrowContainer.appendChild(deleteBtn);
        arrowContainer.appendChild(saveBtn);
        arrowContainer.appendChild(upArrowBtn);
        arrowContainer.appendChild(downArrowBtn);
        // Add in panel
        selectedPanel.appendChild(arrowContainer);
        updateArrowVisibility();
    }

    async function selectPanel(panel)
    {
        if (selectedPanel)
        {
            selectedPanel.classList.remove('selected');
            removeArrows();
        }

        selectedPanel = panel;
        selectedPanel.classList.add('selected');
        await addArrows();
        scrollToMiddle();
    }

    // Unselect panel
    async function unselectPanel() {
        if (selectedPanel) {
            selectedPanel.classList.remove('selected');
            await removeArrows();
            selectedPanel = null;
        }
    }

    async function setupPanels()
    {
        const panels = listPanel.querySelectorAll('.panel');

        panels.forEach(panel =>
        {
            panel.addEventListener('click', async (e) =>
            {
                if (e.detail !== 2)
                {
                    await selectPanel(panel);
                }
            });

            panel.addEventListener('dblclick', unselectPanel);
        });
    }

    async function setPanelOrder()
    {
        const panels = Array.from(listPanel.querySelectorAll('.panel'));

        panelOrder.length = 0;

        panels.forEach((panel, index) =>
        {
            const panelId = panel.dataset.panelId;
            const pageId = panel.dataset.pageId;

            panelOrder.push(
                { "PanelID": panelId, "PageID": pageId, "PanelPosition": index });

        });
    }

    async function enterKeyDown(e)
    {
        if (e.key === 'Enter' && selectedPanel)
        {
            await unselectPanel();
        }
    }
    
    document.addEventListener('keydown', enterKeyDown);

    async function savePanelOrders()
    {
        await setPanelOrder();
        const token = await getValidationToken();

        try
        { 
            $.ajax({
                url: urlUpdatePanelPositon,
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify(panelOrder),
                headers:
                {
                    'RequestVerificationToken': token
                },
                success: function (response)
                {
                    console.log(response);
                },
                error: function (error)
                {
                    console.log(error);
                }
            });
        }
        catch (err)
        {
            console.log(err);
        }
    }

    async function deletePanel(panelId, pageId)
    {
        if (!selectedPanel)
            return;

        const token = await getValidationToken();

        try
        {
            $.ajax({
                url: urlDeletePanel + "?panelId=" + panelId + "&pageId=" + pageId,
                type: 'DELETE',
                dataType: 'json',
                processData: false,
                cache: false,
                async: true,
                headers: {
                    'RequestVerificationToken': token
                },
                success: function (response)
                {
                    console.log(response);
                    location.href = response.receivedUrl;
                },
                error: function (response) {
                    console.log(response);
                }
            });
        }
        catch (err)
        {
            console.log(err);
        }
    }

    await setPanelOrder();

    await setupPanels();

});