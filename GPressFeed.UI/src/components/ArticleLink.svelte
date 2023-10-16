<script lang=ts>
import type  Article from "../models/Article.type";
export let article: Article;
</script>

<style>    
.article{
    display: flex;
    border-left: 5px solid #232323;
    padding-left: 15px;
    min-height: 45px;
    align-items: center;
}

.articleLink {
    color: white;
    font-size: 30px;
    font-weight: 500;
    text-decoration: none;
    transition: 0.2s;
}   

.articleLink:hover {
color: orange;
}   

.articleLinkWrapper {
    transition: 0.2s;
}

.articleLinkWrapper:hover {
    transition: 0.2s;
    margin-left: 20px;
}
@media screen and (max-width: 480px){
    .articleLink {
    font-size: 20px;
    font-weight: 500;
    }  
}
</style>

<div class="article">
    <div class="articleLinkWrapper">
     <a class="articleLink" target="_blank" href={article.link} rel="noreferrer">
          {article.title}
        </a>
    </div>
</div>