// define
var MyComponent = Vue.extend({
  // template: '<div>A custom component!</div>'
})

// register
// Vue.component('my-component', MyComponent)

var ImageFeed = Vue.extend({
  template: `
  <ul id="">
    <li v-for="image in images">
      <image-card v-bind:src="image.src"></image-card>
    </li>
  </ul>
  `,
  data: function(){
    return {
      images: [
        {
          src: "https://placekitten.com/g/200/300"
        },
        {
          src: "https://placekitten.com/g/200/300"
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
// create a root instance
new Vue({
  el: 'body'
});
