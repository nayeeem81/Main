# Contributing to Main

Thank you for your interest in contributing to the Main project! We welcome contributions from the community, especially in the areas of **testing** and **user experience (UX)**.

## How to Contribute

### Testing Contributions

We're looking for contributors to help improve test coverage and quality:

- **Unit Tests**: Write tests for existing components and services
- **Integration Tests**: Create tests that verify interactions between modules
- **E2E Tests**: Develop end-to-end tests for critical user workflows
- **Bug Reports**: Test the application and report any issues you discover
- **Test Documentation**: Help document testing procedures and best practices
- **Black Box Testing**: Test the application functionality without examining internal code structure (see Black Box Testing section below)

### UX Contributions

Help us improve the user experience:

- **UI/UX Feedback**: Test the web application and provide feedback on usability
- **Accessibility**: Ensure the application is accessible to all users
- **Documentation**: Improve user guides and documentation
- **Design Improvements**: Suggest and implement UI/UX enhancements
- **Performance**: Identify and help optimize performance bottlenecks

## Getting Started

1. **Fork the repository** and create a new branch for your work
2. **Review existing issues** to see what needs testing or UX improvements
3. **Set up your development environment** following the project structure (ASP.Net 8.0, MVC, API)
4. **Make your contributions** following the coding standards below
5. **Submit a pull request** with a clear description of your changes

## Black Box Testing Contributions

Black Box testing is a critical part of our quality assurance process. Contributors can help by testing the application's functionality from a user's perspective, without needing to understand the internal code implementation.

### What is Black Box Testing?

Black Box testing focuses on:
- **Input/Output behavior**: Testing how the application responds to various inputs
- **User workflows**: Verifying that user journeys work as expected
- **Edge cases**: Testing boundary conditions and unusual input scenarios
- **Error handling**: Ensuring appropriate error messages and recovery
- **Cross-platform compatibility**: Testing across different browsers, devices, and operating systems
- **Performance**: Monitoring application responsiveness and load times

### How to Contribute Black Box Tests

#### Step 1: Set Up Your Environment

1. Fork the repository to your personal GitHub account
2. Clone your fork locally:
   ```bash
   git clone https://github.com/YOUR-USERNAME/Main.git
   cd Main
   ```
3. Create a new branch for your testing work:
   ```bash
   git checkout -b testing/black-box-testing
   ```
4. Set up the ASP.Net 8.0 application following the README.md instructions

#### Step 2: Create Test Cases

Create a new test documentation file or update existing test files in the `/tests/black-box/` directory:

1. **Test Case Format** (use Markdown or JSON):
   ```markdown
   ### Test Case: [Feature/Functionality Being Tested]
   
   **Objective**: [What you're testing and why]
   
   **Prerequisites**: [Setup requirements, user roles, data needed]
   
   **Steps**:
   1. [Action 1]
   2. [Action 2]
   3. [Action 3]
   
   **Expected Result**: [What should happen]
   
   **Actual Result**: [What actually happened during testing]
   
   **Status**: [Pass/Fail]
   
   **Environment**: [OS, Browser, Device specifications]
   
   **Notes**: [Any additional observations or issues]
   ```

2. **Test Categories** to focus on:
   - **API Endpoints**: Test all API responses with valid and invalid inputs
   - **UI Workflows**: Complete user journeys from login to task completion
   - **Data Validation**: Submit forms with missing, invalid, or edge-case data
   - **Navigation**: Verify all UI navigation paths work correctly
   - **Accessibility**: Test keyboard navigation and screen reader compatibility

#### Step 3: Document Your Findings

Create a detailed test report including:

- **Test Summary**: Overview of what was tested
- **Test Coverage**: Which features/modules were covered
- **Pass Rate**: Percentage of tests that passed
- **Issues Found**: List of bugs or unexpected behaviors discovered
- **Screenshots/Videos**: Visual evidence of test execution (especially for failures)
- **Recommendations**: Suggestions for improvement

Save your report as: `YYYY-MM-DD_BlackBoxTestReport_YourName.md`

#### Step 4: Push and Submit

1. Commit your test cases and findings:
   ```bash
   git add .
   git commit -m "Black Box Testing: [Feature/Module Tested] - [Brief Description]"
   ```

2. Push your changes to your fork:
   ```bash
   git push origin testing/black-box-testing
   ```

3. Open a Pull Request (PR) against the `testing/black-box-testing` branch with:
   - **Title**: `Black Box Testing: [Feature/Module]`
   - **Description**: Summary of what was tested and any issues found
   - **Checklist**:
     - [ ] Test cases are documented clearly
     - [ ] Test environment is specified
     - [ ] All test results are included
     - [ ] Screenshots/evidence included for failures
     - [ ] No sensitive data in test reports

### Testing Branch: `testing/black-box-testing`

We've created a dedicated branch `testing/black-box-testing` for all Black Box testing contributions. This branch is:

- **Isolated**: Separate from the main development branch
- **Collaborative**: Multiple testers can work on this branch simultaneously
- **Non-blocking**: Testing PRs won't affect main feature development
- **Reviewable**: Test results can be reviewed and aggregated before merging to main

**All Black Box testing contributions should be submitted as PRs to the `testing/black-box-testing` branch.**

## Coding Standards

- Follow the existing code structure and naming conventions
- Ensure all tests pass before submitting a PR
- Write clear, descriptive commit messages
- Add comments for complex logic or UX changes
- Maintain compatibility across multiple platforms and containers

## Testing Guidelines

- Write tests for new features and bug fixes
- Aim for good test coverage
- Include both positive and negative test cases
- Test across different browsers and devices for UX work
- For Black Box testing, focus on user-visible behavior and workflows
- Document all test cases with clear steps and expected results
- Report bugs with reproducible steps and environment details

## Reporting Issues

When reporting issues found during testing:

1. Check if the issue already exists
2. Provide a clear, descriptive title
3. Include step-by-step reproduction steps
4. Attach screenshots or screen recordings
5. Specify your testing environment (OS, browser, device)
6. Label as `bug` or `testing-feedback` as appropriate

## Code of Conduct

- Be respectful and constructive in all interactions
- Provide helpful feedback and suggestions
- Collaborate openly with other contributors
- Focus on the goal of improving the project

Thank you for helping make Main better!
