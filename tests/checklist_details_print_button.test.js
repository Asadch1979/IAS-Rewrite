const fs = require('fs');
const path = require('path');

// Simple regression test verifying that the checklist memo editor
// includes a print button and invokes the print preview logic.
// This is a static check since full UI automation is not available.

const viewPath = path.join(__dirname, '..', 'AIS', 'AIS', 'Views', 'Execution', 'checklist_details.cshtml');
const content = fs.readFileSync(viewPath, 'utf8');

function assertContains(pattern, message) {
  if (!pattern.test(content)) {
    console.error(message);
    process.exit(1);
  }
}

// Check for memo print button markup
assertContains(/id\s*=\s*"memoPrintBtn"/, 'memoPrintBtn not found in checklist_details.cshtml');
assertContains(/fa\s+fa-print/, 'fa-print icon missing for print button');

// Ensure printPreview plugin is invoked
assertContains(/printPreview\(/, 'printPreview plugin not invoked for memo print button');

console.log('Checklist memo print button markup and printPreview invocation verified.');
