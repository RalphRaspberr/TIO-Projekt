var ImageViews = Vue.extend({
  props: [
    'user-id',
    'image-id'
  ],
  methods: {
    getViews: function(){
      this.viewResource.get().then(function (response) {
          //  this.$set('images', response);
      });
    }
  },
  ready: function(){
    this.viewResource = this.$resource('stats{/userId}{/imageId}');
  }
});

var UserArea = Vue.extend({
  template: `
  <nav class="navbar navbar-default navbar-fixed-top navigation">
    <div class="container">
      <a class="navbar-brand"><img class="" src="images/bowl.png"></a>
      <a class="brand-name" href="#">Leczo</a>
      <ul v-if="!loggedin" class="nav navbar-nav navbar-right action-list">
        <ul class="nav pull-right user-actions">
          <li class="dropdown" id="menuSignUp">
            <a class="dropdown-toggle" href="#" data-toggle="dropdown" id="navSignUp">Sign Up</a>
            <div class="dropdown-menu signup-modal" style="padding:17px;">
              <form class="form" id="formSignUp">
                <input name="username" id="username" type="text" placeholder="Username">
                <input name="password" id="password" type="password" placeholder="Password"><br>
                <button type="button" id="btnLogin" class="btn">Sign Up</button>
              </form>
            </div>
          </li>
          <li class="dropdown" id="menuLogin">
            <a class="dropdown-toggle" href="#" data-toggle="dropdown" id="navLogin">Login</a>
            <div class="dropdown-menu login-modal" style="padding:17px;">
              <form class="form" id="formLogin">
                <input name="username" id="username" type="text" placeholder="Username">
                <input name="password" id="password" type="password" placeholder="Password"><br>
                <button type="button" id="btnLogin" class="btn">Login</button>
              </form>
            </div>
          </li>
        </ul>
      </ul>

      <ul v-if="loggedin" class="nav navbar-nav navbar-right action-list">
        <li>
          <a href="{{ userProfile }}" data-toggle="dropdown" id="navSignUp">{{ name }}</a>
        </li>
      </ul>
    </div>
  </nav>
  `,
  data: function() {
    return {
      name: 'Adusia',
      userProfile: '#'
    }
  },
  methods: {
    displayName: function(){

    }
  }
});

var ImageFeed = Vue.extend({
  template: `
  <ul id="feed-list">
    <li v-for="image in images">
      <image-card :image="image"></image-card>
    </li>
  </ul>
  `,
  data: function(){
    return {
      images: [
        {
          src: "https://placekitten.com/g/200/300",
          title: "#kicius",
          author: "Adusia",
          authorId: 123,
          authorProfile: "#",
          imageId: 244,
          views: 4
        },
        {
          src: "https://placekitten.com/g/200/200",
          title: "#kicius",
          author: "Adusia",
          authorId: 123,
          authorProfile: "#",
          imageId: 2435,
          views: 4
        }
      ]
    }
  },
  methods: {
    getImages: function(){
      this.imageResource.get().then(function (response) {
          //  this.$set('images', response);
      });
    },
    getUserImages: function(userId) {
      this.imageResource.get({userId: userId}).then(function (response) {
          //  this.$set('images', response);
      });
    },
    getImage: function(userId, imageId){
      this.imageResource.get({userId: userId, imageId: imageId}).then(function (response) {
          //  this.$set('images', response);
      });
    },
    addImage: function(image, title, userId){
      this.imageResource.save({userId: userId}, {image: image, title: title});
    }
  },
  ready: function(){
    this.imageResource = this.$resource('image{/userId}{/imageId}');
  }
});

var ImageCard = Vue.extend({
  template: `
  <div class="thumbnail">
    <img v-bind:src="image.src">
    <div class="caption">
      <h3>{{ image.title }}</h3>
      <p><a v-bind:href="image.profile">{{ image.author }}</a></p>
      <image-views user-id="{{ image.authorId }}" image-id="{{ image.imageId }}"></image-views>
    </div>
  </div>
  `,
  props: [
    'image'
  ]
})

// register
Vue.component('image-views', ImageViews);
Vue.component('image-card', ImageCard);
Vue.component('image-feed', ImageFeed);
Vue.component('user-area', UserArea);
// create a root instance
new Vue({
  el: 'body'
});
