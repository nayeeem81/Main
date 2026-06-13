const listPanel = document.querySelector('.panel-list');
let selectedPanel = null;
const panelOrder = [];

// Initialize panel order array
function initializePanelOrder() {
    const panels = Array.from(listPanel.querySelectorAll('.panel'));
    panelOrder.length = 0;
    panels.forEach((panel, index) => {
        panelOrder.push(index);
    });
}

// Setup event listeners for all panels
function setupPanels() {
    const panels = listPanel.querySelectorAll('.panel');

    panels.forEach(panel => {
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
function selectPanel(panel) {
    // Deselect previous panel
    if (selectedPanel) {
        selectedPanel.classList.remove('selected');
        removeArrows();
    }

    // Select new panel
    selectedPanel = panel;
    selectedPanel.classList.add('selected');

    // Add arrow buttons
    addArrows();

    // Scroll to make panel visible
    ensureVisible();
}

// Ensure selected panel is visible on screen
function ensureVisible() {
    if (!selectedPanel) return;

    selectedPanel.scrollIntoView({
        behavior: 'smooth',
        block: 'nearest'
    });
}

// Add up and down arrow buttons to selected panel
function addArrows() {
    const existingArrows = selectedPanel.querySelector('.arrow-buttons');
    if (existingArrows) return;

    const arrowContainer = document.createElement('div');
    arrowContainer.className = 'arrow-buttons';

    // Up arrow button
    const upArrowBtn = document.createElement('button');
    upArrowBtn.className = 'arrow-btn up-arrow-btn';
    upArrowBtn.innerHTML = '↑';
    upArrowBtn.title = 'Move up';
    upArrowBtn.setAttribute('aria-label', 'Move up');

    upArrowBtn.addEventListener('click', (e) => {
        e.stopPropagation();
        moveUp();
    });

    // Down arrow button
    const downArrowBtn = document.createElement('button');
    downArrowBtn.className = 'arrow-btn down-arrow-btn';
    downArrowBtn.innerHTML = '↓';
    downArrowBtn.title = 'Move down';
    downArrowBtn.setAttribute('aria-label', 'Move down');

    downArrowBtn.addEventListener('click', (e) => {
        e.stopPropagation();
        moveDown();
    });

    arrowContainer.appendChild(upArrowBtn);
    arrowContainer.appendChild(downArrowBtn);
    selectedPanel.appendChild(arrowContainer);

    // Update arrow visibility
    updateArrowVisibility();
}

// Remove arrow buttons
function removeArrows() {
    const arrowContainer = selectedPanel.querySelector('.arrow-buttons');
    if (arrowContainer) arrowContainer.remove();
}

// Update arrow button visibility based on position
function updateArrowVisibility() {
    if (!selectedPanel) return;

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
        } else {
            upArrowBtn.classList.remove('disabled');
            upArrowBtn.disabled = false;
        }
    }

    // Hide/disable down arrow if last
    if (downArrowBtn) {
        if (isLast) {
            downArrowBtn.classList.add('disabled');
            downArrowBtn.disabled = true;
        } else {
            downArrowBtn.classList.remove('disabled');
            downArrowBtn.disabled = false;
        }
    }
}

// Move selected panel up one step
function moveUp() {
    if (!selectedPanel) return;

    const panels = Array.from(listPanel.querySelectorAll('.panel'));
    const currentIndex = panels.indexOf(selectedPanel);

    if (currentIndex > 0) {
        const prevPanel = selectedPanel.previousElementSibling;
        listPanel.insertBefore(selectedPanel, prevPanel);

        // Update order array and arrow visibility
        updatePanelOrder();
        updateArrowVisibility();

        // Ensure panel stays visible on screen after move
        ensureVisible();
    }
}

// Move selected panel down one step
function moveDown() {
    if (!selectedPanel) return;

    const panels = Array.from(listPanel.querySelectorAll('.panel'));
    const currentIndex = panels.indexOf(selectedPanel);

    if (currentIndex < panels.length - 1) {
        const nextPanel = selectedPanel.nextElementSibling;
        listPanel.insertBefore(nextPanel, selectedPanel);

        // Update order array and arrow visibility
        updatePanelOrder();
        updateArrowVisibility();

        // Ensure panel stays visible on screen after move
        ensureVisible();
    }
}

// Update panel order array
function updatePanelOrder() {
    const panels = Array.from(listPanel.querySelectorAll('.panel'));
    panelOrder.length = 0;
    panels.forEach((panel, index) => {
        var panelId = panel.id;
        panelOrder.push({ "PanelID" : panelId, "PanelPosition" : index });
    });

    console.log('Current panel order:', panelOrder);
}

// Unselect panel
function unselectPanel() {
    if (selectedPanel) {
        selectedPanel.classList.remove('selected');
        removeArrows();
        selectedPanel = null;
    }
}

// Listen for Enter key to unselect
document.addEventListener('keydown', (e) => {
    if (e.key === 'Enter' && selectedPanel) {
        unselectPanel();
    }
});

// Initialize on DOM ready
document.addEventListener('DOMContentLoaded', () => {
    initializePanelOrder();
    setupPanels();
});