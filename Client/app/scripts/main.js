// define
var MyComponent = Vue.extend({
  // template: '<div>A custom component!</div>'
})

// register
// Vue.component('my-component', MyComponent)


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
      <div class="thumbnail">
        <image-card v-bind:src="image.src"></image-card>
          <div class="caption">
            <h3>{{ image.title }}</h3>
            <p><a v-bind:href="image.authorProfile">{{ image.author }}</a></p>
            <p class="views">{{ image.views }}</p>
          </div>
      </div>
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
          authorProfile: "#",
          views: 4
        },
        {
          src: "https://placekitten.com/g/200/200",
          title: "#kicius",
          author: "Adusia",
          authorProfile: "#",
          views: 4
        }
      ]
    }
  }
});

var ImageCard = Vue.extend({
  template: '<div><img v-bind:src="src"></div>',
  props: [
    'src'
  ]
})

// register
Vue.component('image-card', ImageCard);
Vue.component('image-feed', ImageFeed);
Vue.component('user-area', UserArea);
// create a root instance
new Vue({
  el: 'body'
});
