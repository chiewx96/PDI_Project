<template>
  <v-content>
    <v-container
      fluid
      fill-height
    >
      <v-layout
        align-center
        justify-center
      >
        <div
          class="justify-center min-width"
          size="md"
        >
          <v-text-field
            class="col-lg-3 col-md-6 col-sm-12"
            required
            hide-details="auto"
            v-model="decoded_batch_no"
            label="Batch No"
            prepend-icon="mdi-qrcode"
            @keydown.enter.prevent="cancelOutbound"
          ></v-text-field>
          <v-checkbox
            v-model="hide_scanner"
            value="1"
            false-value="0"
            label="Hide Camera"
          >
          </v-checkbox>
          <qrcode-stream
            v-show="hide_scanner == 0"
            @init="onInit"
            @decode="onDecode"
            :torch="torch"
            :camera="camera"
          ></qrcode-stream>
        </div>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script>
import { QrcodeStream } from 'vue3-qrcode-reader';
import ApiService from '@/services/api.service';
import Swal from 'sweetalert2';

export default {
  components: { QrcodeStream },
  data() {
    return {
      error: '',
      decoded_batch_no: '',
      torch: false,
      fetchData: '',
      camera: 'auto',
      hide_scanner: 0,
    };
  },
  computed: {
    computed_outbound_model() {
      let model = {
        ContainerId: this.container_id,
        PackageReferenceNo: this.scanned_items,
      };
      return model;
    },
  },
  methods: {
    async onInit(promise) {
      try {
        await promise;
      } catch (error) {
        if (error.name === 'NotAllowedError') {
          this.error = 'ERROR: you need to grant camera access permission';
        } else if (error.name === 'NotFoundError') {
          this.error = 'ERROR: no camera on this device';
        } else if (error.name === 'NotSupportedError') {
          this.error = 'ERROR: secure context required (HTTPS, localhost)';
        } else if (error.name === 'NotReadableError') {
          this.error = 'ERROR: is the camera already in use?';
        } else if (error.name === 'OverconstrainedError') {
          this.error = 'ERROR: installed cameras are not suitable';
        } else if (error.name === 'StreamApiNotSupportedError') {
          this.error = 'ERROR: Stream API is not supported in this browser';
        } else if (error.name === 'InsecureContextError') {
          this.error =
            'ERROR: Camera access is only permitted in secure context. Use HTTPS or localhost rather than HTTP.';
        } else {
          this.error = `ERROR: Camera error (${error.name})`;
        }
      } finally {
        this.showScanConfirmation = this.camera === 'off';
      }
    },
    async onDecode(result) {
      this.decoded_batch_no = result;
      Swal.fire({
        title: "Are you sure?",
        text: `Are you sure you want to cancel batch number : ${this.decoded_batch_no }?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
      }).then((result) => {
        if (result.isConfirmed) {
          this.cancelOutbound();
        }
      });
    },
    cancelOutbound() {
      ApiService._get(`outbound/cancel/${this.decoded_batch_no}`)
        .then(async (response) => {
          let result = await response.json();
          if (response.status == 200) {
            Swal.fire({
              icon: 'success',
              title: `Outbound Batch No has been cancelled : ${this.decoded_batch_no} `,
            });
            this.decoded_batch_no = '';
          } else {
            Swal.fire({
              icon: 'error',
              title: 'Cancellation Error.',
              text: 'The provided batch number is not found/has not been outbound.',
            });
          }
        })
        .catch(() => {
          Swal.fire({
            icon: 'error',
            title: 'Network Error. Could not connect to api service.',
            text: 'Contact Administrator!',
          });
        });
    },
    async reloadPage() {
      await this.timeout(500);
      window.location.reload();
    },
    timeout(ms) {
      return new Promise((resolve) => {
        window.setTimeout(resolve, ms);
      });
    },
  },
};
</script>
<style scoped>
.scan-confirmation {
  position: absolute;
  width: 100%;
  height: 100%;

  background-color: rgba(255, 255, 255, 0.8);

  display: flex;
  flex-flow: row nowrap;
  justify-content: center;
}

.min-width {
  min-width: 800px;
  margin-top: 50px;
  margin: auto;
}
</style>
