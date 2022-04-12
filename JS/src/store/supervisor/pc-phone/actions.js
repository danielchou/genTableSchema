import axios from '@boot/axios';
import fileDownload from 'js-file-download';

const apiUrlPrefix = '/api/PcPhone';

export const fetchItems = ({ rootGetters, commit }, data) => {
  /**
   * Request body:
   *  None
   */
  const opts = {
    method: 'get',
    url: `${apiUrlPrefix}/query`,
    data: {
      ...rootGetters['app/getDefaultListParams'],
      ...data,
    },
  };

  const response = axios.webapi({ opts, commit }).then((res) => {
    commit('setItems', res.data || []);
    return res;
  });

  return response;
};

export const fetchItemById = ({ commit }, seqNo) => {
  /**
   * Request body:
   *  seqNo : Integer *Required
   */
  const opts = {
    method: 'get',
    url: `${apiUrlPrefix}/${seqNo}`,
  };

  const response = axios.webapi({ opts, commit }).then((res) => {
    commit('setItem', res.data);
    return res.data;
  });

  return response;
};

export const fetchCreate = ({ commit }, data) => {
  /**
   * Request body:
   	* computerIP                :  String
	* computerName              :  String
	* extCode                   :  String
	* memo                      :  String
   */
  const opts = {
    method: 'post',
    url: `${apiUrlPrefix}`,
    data,
  };

  const response = axios.webapi({ opts, commit }).then((res) => res);

  return response;
};

export const fetchUpdate = ({ commit }, data) => {
  /**
   * Request body:
   	* computerIP                :  String
	* computerName              :  String
	* extCode                   :  String
	* memo                      :  String
   */

  const opts = {
    method: 'put',
    url: `${apiUrlPrefix}`,
    data,
  };

  const response = axios.webapi({ opts, commit }).then((res) => res);

  return response;
};

export const fetchDelete = ({ commit }, seqNo) => {
  /**
   * Request body:
   *  seqNo : Integer
   */
  const opts = {
    method: 'delete',
    url: `${apiUrlPrefix}`,
    data: {
      seqNo,
    },
  };

  const response = axios.webapi({ opts, commit }).then((res) => res);

  return response;
};

export const fetchExportReport = ({ commit }, data) => {
  /**
   * Request body:
   * isEncrypt : Boolean,
   * pData     : String,
   * reportName: String,
   * jsonData  : String Object,
   */

  const { isEncrypt = false, pData = '', reportName = 'rptPcPhone', jsonData = {} } = data;

  const opts = {
    method: 'post',
    url: `/api/Report`,
    responseType: 'blob',
    data: {
      isEncrypt,
      pData,
      reportName,
      jsonData: JSON.stringify(jsonData),
    },
  };

  const response = axios.webapi({ opts, commit }).then((res) => {
    fileDownload(res, isEncrypt ? `${reportName}.zip` : `${reportName}.xls`, res.type);
    return res;
  });

  return response;
};
