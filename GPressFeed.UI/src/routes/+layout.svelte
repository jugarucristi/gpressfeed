<style>
	@import 'layout.css';
</style>

<div class="header">
    <div class="row">
	    <a class="logo" href="/">GPressFeed</a> 
        <div class="typography" style="margin: 10px; font-size: 50px">&#x2022</div>
    </div>
    <!--<div class="headerbutton">Older News</div>-->
	<a class="headerbutton" href="/about">About</a>
</div>
<div class="pagecontent">
    <slot/>
</div>
<div class="footer">Cristi Jugaru &#x2022 {new Date().getFullYear()}</div>