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
              <form class="form" v-on:submit.prevent="register" id="formSignUp">
                <input name="username" id="username" type="text" placeholder="Username"  v-model="userName">
                <input name="password" id="password" type="password" placeholder="Password" v-model="password"><br>
                <button type="submit" id="btnLogin" class="btn">Sign Up</button>
              </form>
            </div>
          </li>
          <li class="dropdown" id="menuLogin">
            <a class="dropdown-toggle" href="#" data-toggle="dropdown" id="navLogin">Login</a>
            <div class="dropdown-menu login-modal" style="padding:17px;">
              <form class="form" id="formLogin" v-on:submit.prevent="login">
                <input name="username" id="username" type="text" placeholder="Username" v-model="userName">
                <input name="password" id="password" type="password" placeholder="Password" v-model="password"><br>
                <button type="submit" id="btnLogin" class="btn">Login</button>
              </form>
            </div>
          </li>
        </ul>
      </ul>

      <ul v-if="loggedin" class="nav navbar-nav navbar-right action-list">
        <li class="dropdown" id="addPicture">
          <a class="dropdown-toggle addPic" href="#" data-toggle="dropdown" id="navLogin">Add Picture</a>
          <div class="dropdown-menu login-modal" style="padding:17px;">
            <form class="form" id="uploadForm" v-on:submit.prevent="addPicture">
              <input name="title" id="title" type="text" placeholder="Title" v-model="title">
              <input name="picture" id="picture" type="file" placeholder="Password" v-el:picture accept="image/gif, image/jpeg, image/png"><br>
              <button type="submit" id="addPicBtn" class="btn">Submit</button>
            </form>
          </div>
          </li>
        <li>
          <button type="submit" class="btn btnLogout" v-on:click="logout">Logout</button>
        </li>
        <li>
          <button type="submit" class="usun-konto" v-on:click="">USUŃ KONTO ( ͡° ͜ʖ ͡°)</button
        </li>
        <li>
          <a href="{{ userProfile }}" data-toggle="dropdown" id="navSignUp">{{ name }}</a>
        </li>
      </ul>
    </div>
  </nav>
  `,
  data: function() {
    return {
      userName: '',
      password: '',
      loggedin: false,
      title: ''
    }
  },
  methods: {
    register: function(){
      this.accountResource.save({accountAction: 'Register'}, {
        Email: this.userName + '@leczo.io',
        Password: this.password,
        ConfirmPassword: this.password
      }).then(function(){
        this.login();

      });
      console.log(this.userName, this.password);
    },
    login: function(){
      Vue.http.options.emulateJSON = true;
      this.tokenResource.save({}, {
        grant_type: 'password',
        username: this.userName + '@leczo.io',
        password: this.password
      }).then(function(response){
        Vue.http.headers.common['Authorization'] = `Bearer ${response.data.access_token}`;
        sessionStorage.setItem('token', response.data.access_token);
        this.$set('loggedin', true);
      });
      Vue.http.options.emulateJSON = false;
    },
    removeAccount: function(){
      this.accountResource.save({},{});
    },
    logout: function(){
       sessionStorage.removeItem('token');
       this.$set('loggedin', false);
    },
    addPicture: function(){
      const formData = new FormData();
      formData.append('Picture', this.$els.picture.files[0]);
      formData.append('Title', this.title);
      formData.append('Author', this.userName);
      this.imageResource.save({}, formData);
    }
  },
  ready: function(){
    this.imageResource = this.$resource('http://localhost:57146/api/Images');
    this.accountResource = this.$resource('http://localhost:57146/api/Account{/accountAction}');
    this.tokenResource = this.$resource('http://localhost:57146/Token');
    const token = sessionStorage.getItem('token');
    if(token){
      Vue.http.headers.common['Authorization'] = `Bearer ${token}`;
      this.$set('loggedin', true);
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
            console.log(response.data);
           this.$set('images', response.data);
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
    this.imageResource = this.$resource('http://localhost:57146/image{/userId}{/imageId}');
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
});

// register
Vue.component('image-views', ImageViews);
Vue.component('image-card', ImageCard);
Vue.component('image-feed', ImageFeed);
Vue.component('user-area', UserArea);
// create a root instance
new Vue({
  el: 'body'
});
