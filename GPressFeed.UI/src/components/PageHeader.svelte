<style>
.header{
display: flex;
box-sizing: border-box;
flex-direction: row;
align-items: center;
justify-content: space-between;
width: 100vw;
background: linear-gradient(90deg, rgba(0,0,0,1) 0%, rgba(23,23,23,1) 94%);
height: 4em;
border-bottom: 1px solid white;
padding-left: 20px;
padding-right: 20px;
overflow: hidden;
}

.headerbutton {
text-decoration: none;
color: white;
font-size: 20px;
font-weight: 600;
display: flex;
justify-content: center;
align-items: center;
width: 100px;
height: 80%;
margin: 10px;
border-radius: 10px;
overflow: hidden;
}

.logo {
    text-decoration: none;
    font-size: 50px;
    font-weight: 1000;
    color: white;
}

@media screen and (min-width: 480px)
{
    .logo:hover {
        color: orange;
        cursor: pointer;
        transition: 0.2s;
    }
    .dropdownmenuactive{
        height: 0px;
    }
}

.separatorDot{
    color: white;
    margin: 10px; 
    font-size: 50px
}

.hamburger-menu {
    display: none;
    width: 30px;
    height: 25px;
    margin-top: 5px;
    border-radius: 3px;
    background: linear-gradient(
      to bottom, 
      white, white 20%, 
      black 20%, black 40%, 
      white 40%, white 60%, 
      black 60%, black 80%, 
      white 80%, white 100%
    );
}

.hamburger-menu-active {
    background: linear-gradient(
      to bottom, 
      orange, orange 20%, 
      black 20%, black 40%, 
      orange 40%, orange 60%, 
      black 60%, black 80%, 
      orange 80%, orange 100%
    );
}

@media screen and (max-width: 480px){
    .header{
    display: flex;
    justify-content: space-between;
    padding: 0px 10% 0px;
    box-sizing: border-box;
    width: 100vw;
    background: linear-gradient(90deg, rgba(0,0,0,1) 0%, rgba(23,23,23,1) 94%);
    height: 5em;
    border-bottom: 1px solid white;
    overflow: hidden;
    }
    .logo {
    text-decoration: none;
    font-size: 45px;
    font-weight: 1000;
    color: white;
    }
    .headerbutton{
      display: none;
    }
    .separatorDot{
        display: none;
    }
    .hamburger-menu{
        display: block;
    }
}

.headerbutton:hover {
    color: orange;
    cursor: pointer;
    transition: 0.2s;
}

.dropdownmenu{
    background: #232323;
    border-bottom: 1px solid white;
    display:flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 300px;
    transform: 1s;
    height: 0px;
    transition: 0.5s;
}

.dropdownmenuactive{
    height: 225px;
    padding: 15px;
}

.dropdownbutton {
    text-decoration: none;
    color: white;
    font-size: 25px;
    font-weight: 600;
    margin: 20px;
}
</style>

<script>
    let isMenuActive = false;
</script>

<div class="header" class:headerdropdownactive={isMenuActive}>
    <div class="row">
	    <a class="logo" on:click={(_) => isMenuActive = false} href="/">GPressFeed</a> 
        <div class="separatorDot">
            &#x2022
        </div>
        <a class="headerbutton" href="/">Home</a>
        <a class="headerbutton" href="/archive">Older News</a>
    </div>
    <a class="headerbutton" href="/about">About</a>
    <div aria-hidden="true" on:click={(_) => isMenuActive = !isMenuActive} class="hamburger-menu" class:hamburger-menu-active={isMenuActive}/>
</div>
<div class="dropdownmenu" class:dropdownmenuactive={isMenuActive}>
    <a class="dropdownbutton" on:click={(_) => isMenuActive = !isMenuActive} href="/">Home</a>
    <a class="dropdownbutton" on:click={(_) => isMenuActive = !isMenuActive} href="/archive">Older News</a>
    <a class="dropdownbutton" on:click={(_) => isMenuActive = !isMenuActive} href="/about">About</a>
</div>
