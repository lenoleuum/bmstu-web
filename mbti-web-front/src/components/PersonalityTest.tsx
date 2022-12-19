import NavigationBar from "./NavigationBar";

const PersonalityTest = () =>
{
    return (
        <div>
        <NavigationBar></NavigationBar>
        <div className="text-center">
            <h2 className="mt-5">
                Want do discover your <text className="text-warning">MBTI</text> type? 
            </h2>
            <img className="img-responsive pr-2" width="40" src={require("../img/link.png")} />
            <a className="fs-1 mt-5" href="https://www.truity.com/test/type-finder-personality-test-new">
                Take personality test!
            </a>
        </div>
        </div>
    );
}

export default PersonalityTest;