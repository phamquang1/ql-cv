import { NCCTalentManagementTemplatePage } from './app.po';

describe('NCCTalentManagement App', function() {
  let page: NCCTalentManagementTemplatePage;

  beforeEach(() => {
    page = new NCCTalentManagementTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
