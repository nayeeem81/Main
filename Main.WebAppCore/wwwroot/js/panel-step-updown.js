
let selectedPanel = null;
const panelOrder = [];

// Initialize on DOM ready
document.addEventListener('DOMContentLoaded', async () =>
{
    function getElementDataId(panelElement) {
        if (!panelElement)
            return;

        const id = panelElement.dataset.panelId;

        return id;
    }
    
    const listPanel = document.querySelector('.list-panel');

    async function enterKeyDown(e)
    {
        if (e.key === 'Enter' && selectedPanel)
        {
            await unselectPanel();
        }
    }
    
    document.addEventListener('keydown', enterKeyDown);
    
    async function initializePanelOrder()
    {

        const panels = Array.from(listPanel.querySelectorAll('.panel'));

        panelOrder.length = 0;

        panels.forEach( (panel, index) =>
        {
            const panelId = panel.dataset.panelId;
            const pageId = panel.dataset.panelId;

            panelOrder.push (
                { "PanelID": panelId, "PageID": pageId, "PanelPosition": index, "Company": 0, "Country": 0 });

        });
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

    
    function scrollToMiddle()
    {
        if (!selectedPanel)
           return;

        selectedPanel.scrollIntoView ({
            behavior: 'smooth',
            block: 'center'
        });
    }


    
    async function addArrows()
    {
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

        upArrowBtn.addEventListener('click', async (e) =>
        {
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

        saveBtn.addEventListener('click', async (e) =>
        {
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

            await unselectPanel();
        });

        arrowContainer.appendChild(deleteBtn);
        arrowContainer.appendChild(saveBtn);
        arrowContainer.appendChild(upArrowBtn);
        arrowContainer.appendChild(downArrowBtn);

        selectedPanel.appendChild(arrowContainer);
        
        updateArrowVisibility();
    }


    async function GetPanelPositionData()
    {
        const panels = Array.from(listPanel.querySelectorAll('.panel'));

        panelOrder.length = 0;

        panels.forEach((panel, index) =>
        {

            const panelId = panel.dataset.panelId;
            const pageId = panel.dataset.panelId;

            panelOrder.push(
                { "PanelID": panelId, "PageID": pageId, "PanelPosition": index, "Company": 0, "Country": 0 });
        });

        console.log(panelOrder);

        return panelOrder;
    }


    async function savePanelOrders()
    {

        const data = await GetPanelPositionData();

        const urlUpdateOprders = '@Url.Action("UpdatePositions", "Pages", new { area = "PageContent" })';

        const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');

        if (!tokenElement)
        {
            console.log("Anti-forgery token element not found in the DOM.");
            return;
        }

        try
        {
            $.ajax({
                url: urlUpdateOprders,
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify(data),
                headers: {
                    'RequestVerificationToken': tokenElement.value
                },
                success: function (response) {
                    console.log('Success:', response);
                },
                error: function (error) {
                    console.error(error.error);
                }
            });
        }
        catch (err)
        {
            console.log(err);
        }
    }

    
    function removeArrows()
    {
        const arrowContainer = selectedPanel.querySelector('.arrow-buttons');

        if (arrowContainer)
            arrowContainer.remove();
    }

    
    function updateArrowVisibility()
    {
        if (!selectedPanel)
            return;

        const panels = Array.from(listPanel.querySelectorAll('.panel'));
        const currentIndex = panels.indexOf(selectedPanel);
        const isFirst = currentIndex === 0;
        const isLast = currentIndex === panels.length - 1;

        const upArrowBtn = selectedPanel.querySelector('.up-arrow-btn');
        const downArrowBtn = selectedPanel.querySelector('.down-arrow-btn');

        // Hide/disable up arrow if first
        if (upArrowBtn)
        {
            if (isFirst)
            {
                upArrowBtn.classList.add('disabled');
                upArrowBtn.disabled = true;
            }
            else
            {
                upArrowBtn.classList.remove('disabled');
                upArrowBtn.disabled = false;
            }
        }

        // Hide/disable down arrow if last
        if (downArrowBtn)
        {
            if (isLast)
            {
                downArrowBtn.classList.add('disabled');
                downArrowBtn.disabled = true;
            }
            else
            {
                downArrowBtn.classList.remove('disabled');
                downArrowBtn.disabled = false;
            }
        }
    }


    // Move selected panel up one step
    async function moveUp()
    {
        if (!selectedPanel)
            return;

        const panels = Array.from(listPanel.querySelectorAll('.panel'));

        const currentIndex = panels.indexOf(selectedPanel);

        if (currentIndex > 0)
        {
            const prevPanel = selectedPanel.previousElementSibling;

            listPanel.insertBefore(selectedPanel, prevPanel);

            // Update order array and arrow visibility
            await updatePanelOrder();

            updateArrowVisibility();

            // Keep panel centered on screen after move
            scrollToMiddle();
        }
    }


    // Move selected panel down one step
    async function moveDown()
    {
        if (!selectedPanel)
            return;

        const panels = Array.from(listPanel.querySelectorAll('.panel'));

        const currentIndex = panels.indexOf(selectedPanel);

        if (currentIndex < panels.length - 1)
        {
            const nextPanel = selectedPanel.nextElementSibling;
            listPanel.insertBefore(nextPanel, selectedPanel);

            // Update order array and arrow visibility
            await updatePanelOrder();

            updateArrowVisibility();

            // Keep panel centered on screen after move
            scrollToMiddle();
        }
    }


    // Update panel order array 
    async function updatePanelOrder()
    {
        const panels = Array.from(listPanel.querySelectorAll('.panel'));

        panelOrder.length = 0;

        panels.forEach((panel, index) =>
        {

            const panelId = panel.dataset.panelId;
            const pageId = panel.dataset.panelId;

            panelOrder.push(
                { "PanelID": panelId, "PageID": pageId, "PanelPosition": index, "Company": 0, "Country": 0 });
        });

        console.log(panelOrder);
    }


    // Unselect panel
    async function unselectPanel()
    {
        if (selectedPanel)
        {
            selectedPanel.classList.remove('selected');

            await removeArrows();

            selectedPanel = null;
        }
    }
   

    function deleteElementById(elementId)
    {
        if (selectedPanel)
        {
            selectedPanel = null;
        }

        const element = document.getElementById(elementId);

        if (element)
        {
            element.remove();
        } 
    }

    async function deletePanel(panelId, pageId)
    {
        if (!selectedPanel)
            return;

        const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');

        if (!tokenElement)
        {
            console.log("Anti-forgery token element not found in the DOM.");
            return;
        }

        const tokenValue = tokenElement.value;

        const urlDeletePanel = '@Url.Action("DeletePanel", "Pages", new { area = "PageContent", panelId = panelId, pageId = pageId })';

        try
        {
            $.ajax({
                url: urlDeletePanel,
                type: 'DELETE',
                dataType: 'json',
                headers: {
                    'RequestVerificationToken': tokenValue
                },
                success: function (response)
                {
                    console.log('Success:', response);
                    const deletedId = response.id;
                    deleteElementById(deletedId);
                },
                error: function (response) {
                    console.log(response.error);
                }
            });
        }
        catch (err)
        {
            console.log(err);
        }
    }

    await initializePanelOrder();

    await setupPanels();

});