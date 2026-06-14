const listPanel = document.querySelector('.list-panel');
let selectedPanel = null;
const panelOrder = [];

// Initialize panel order array
function initializePanelOrder()
{
    const panels = Array.from(listPanel.querySelectorAll('.panel'));
    panelOrder.length = 0;
    panels.forEach((panel, index) =>
    {
        panelOrder.push(index);
    });
}

// Setup event listeners for all panels
function setupPanels()
{
    const panels = listPanel.querySelectorAll('.panel');

    panels.forEach(panel =>
    {
        // Single click to select
        panel.addEventListener('click', (e) => {
            if (e.detail !== 2) { // Ignore double-clicks
                selectPanel(panel);
            }
        });

        // Double-click to unselect
        panel.addEventListener('dblclick', unselectPanel);
    });
}

// Select a panel
function selectPanel(panel)
{
    // Deselect previous panel
    if (selectedPanel)
    {
        selectedPanel.classList.remove('selected');
        removeArrows();
    }

    // Select new panel
    selectedPanel = panel;
    selectedPanel.classList.add('selected');

    // Add arrow buttons
    addArrows();

    // Scroll to middle of screen
    scrollToMiddle();
}

// Scroll selected panel to middle of screen
function scrollToMiddle()
{
    if (!selectedPanel) return;

    selectedPanel.scrollIntoView({
        behavior: 'smooth',
        block: 'center'
    });
}





// Add up and down arrow buttons to selected panel
function addArrows()
{
    const existingArrows = selectedPanel.querySelector('.arrow-buttons');

    if (existingArrows)
        return;

    const arrowContainer = document.createElement('div');
    arrowContainer.className = 'arrow-buttons';

    // Up arrow button
    const upArrowBtn = document.createElement('button');
    upArrowBtn.className = 'arrow-btn up-arrow-btn';
    upArrowBtn.innerHTML = '↑';
    upArrowBtn.title = 'Move up';
    upArrowBtn.setAttribute('aria-label', 'Move up');

    upArrowBtn.addEventListener('click', (e) =>
    {
        e.stopPropagation();
        moveUp();
    });

    // Down arrow button
    const downArrowBtn = document.createElement('button');
    downArrowBtn.className = 'arrow-btn down-arrow-btn';
    downArrowBtn.innerHTML = '↓';
    downArrowBtn.title = 'Move down';
    downArrowBtn.setAttribute('aria-label', 'Move down');

    downArrowBtn.addEventListener('click', (e) =>
    {
        e.stopPropagation();
        moveDown();
    });

    // Save button
    const saveBtn = document.createElement('button');
    saveBtn.className = 'save-btn';
    saveBtn.innerHTML = 'Save';
    saveBtn.title = 'Save or Press Enter';
    saveBtn.setAttribute('aria-label', 'Save or Press Enter');

    saveBtn.addEventListener('click', (e) =>
    {
        e.stopPropagation();
       
        savePanelOrders();
    });

    // Delete button
    const deleteBtn = document.createElement('button');
    deleteBtn.className = 'delete-btn';
    deleteBtn.id = selectedPanel.id;
    deleteBtn.innerHTML = 'Delete';
    deleteBtn.title = 'Click to Delete Panel';
    deleteBtn.setAttribute('aria-label', 'Click to Delete Panel');

    deleteBtn.addEventListener('click', (e) =>
    {
        e.stopPropagation();

        deletePanel(e.target.id);
    });

    arrowContainer.appendChild(deleteBtn);
    arrowContainer.appendChild(saveBtn);
    arrowContainer.appendChild(upArrowBtn);
    arrowContainer.appendChild(downArrowBtn);
    
    selectedPanel.appendChild(arrowContainer);

    // Update arrow visibility
    updateArrowVisibility();
}

async function deletePanel(id)
{
    if (!selectedPanel)
        return;

    console.log("Delete Panel Id:" + id);

    try
    {

        const response =

            await fetch( '@Url.Action("DeletePanel", "Pages", new { area = "PageContent" })' + $ { id },
            {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

        if (response.ok)
        {
            console.log(`Product ${id} successfully deleted.`);

            const element = document.getElementById(id);

            if (element)
            {
                element.remove();
                unselectPanel();

                return true;
            }
        }
        else if (response.status === 404)
        {
            console.error('The item could not be found on the server.');
        }
        else
        {
            console.error('An error occurred while trying to delete the item.');
        }
    }
    catch (error)
    {
        console.error('Network error or request failure:', error);
    }

    return false;
}

function GetPanelPositionData()
{
    const panels = listPanel.querySelectorAll('.panel');

    let count = 0;
    let panelId = 0;
    const listPanelOrder = [];

    panels.forEach(panel =>
    {
        count += 1;
        panelId = panel.id;

        listPanelOrder.push({ "PanelID": panelId, "PanelPosition": count })
    });

    return listPanelOrder;
}

async function savePanelOrders()
{

    const data = GetPanelPositionData();

    const urlUpdateOprders = '@Url.Action("UpdatePositions", "Pages", new { area = "PageContent" })';

    try
    {
        const response =

            await fetch( urlUpdateOprders ,
            {
                method: 'POST' ,
                headers: { 'Content-Type': 'application/json' } ,
                body: JSON.stringify(data)
            } );

        if (response.ok)
        {
            const result = await response.json();

            unselectPanel();

            console.log("Success:", result.success);
        }
        else
        {
            console.error("Server Error:", response.success, response.error);
        }
    }
    catch (error)
    {
        console.error("Network Error:", error);
    }

}

// Remove arrow buttons
function removeArrows()
{
    const arrowContainer = selectedPanel.querySelector('.arrow-buttons');

    if (arrowContainer)
        arrowContainer.remove();
}

// Update arrow button visibility based on position
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
        if (isFirst) {
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
function moveUp()
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
        updatePanelOrder();
        updateArrowVisibility();

        // Keep panel centered on screen after move
        scrollToMiddle();
    }
}

// Move selected panel down one step
function moveDown()
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
        updatePanelOrder();
        updateArrowVisibility();

        // Keep panel centered on screen after move
        scrollToMiddle();
    }
}

// Update panel order array
function updatePanelOrder()
{
    const panels = Array.from(listPanel.querySelectorAll('.panel'));
    panelOrder.length = 0;

    panels.forEach((panel, index) => {
        panelOrder.push(index);
    });

    console.log('Current panel order:', panelOrder);
}

// Unselect panel
function unselectPanel()
{
    if (selectedPanel)
    {
        selectedPanel.classList.remove('selected');
        removeArrows();
        selectedPanel = null;
    }
}


// Listen for Enter key to unselect
document.addEventListener('keydown', (e) =>
{
    if (e.key === 'Enter' && selectedPanel) {
        unselectPanel();
    }
});

// Initialize on DOM ready
document.addEventListener('DOMContentLoaded', () =>
{
    initializePanelOrder();
    setupPanels();
});